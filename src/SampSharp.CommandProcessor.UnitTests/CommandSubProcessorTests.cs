// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections;
using System.Collections.Generic;
using Moq;
using Shouldly;
using Xunit;

namespace SampSharp.CommandProcessor.UnitTests;

public class CommandSubProcessorTests
{
    [Fact]
    public void AddCommand_should_add_command()
    {
        // arrange
        var sut = new CommandSubProcessor(null);

        var parser = Mock.Of<ICommandParser>();

        // act
        sut.AddCommand(parser);

        // assert
        sut.ShouldHaveSingleItem()
            .ShouldBe(parser);
    }
    
    [Fact]
    public void Enumerator_should_return_nested_collections()
    {
        // arrange
        var nestedCommand = Mock.Of<ICommandParser>();
        var nestedCommandCollection = new Mock<ICommandParserAndCollection>();

        nestedCommandCollection.Setup(x => x.GetEnumerator())
            .Returns(() => new List<ICommandParser> { nestedCommand }.GetEnumerator());

        var sut = new CommandSubProcessor(null);
        sut.AddCommand(nestedCommandCollection.Object);

        // act / assert
        sut.ShouldHaveSingleItem()
            .ShouldBe(nestedCommand);
    }

    [Fact]
    public void IEnumerable_enumerator_should_succeed()
    {
        // arrange
        var sut = new CommandSubProcessor(null);
        sut.AddCommand(Mock.Of<ICommandParser>());
        
        // act
        var enumerator = ((IEnumerable)sut).GetEnumerator();

        // assert
        enumerator.MoveNext()
            .ShouldBeTrue();

        enumerator.MoveNext()
            .ShouldBeFalse();
    }

    [Theory]
    [InlineData("hello")]
    [InlineData("goodbye")]
    public void Parse_should_succeed(string commandText)
    {
        // arrange
        var context = CreateContext(false);

        var sut = CreateSutWithTestCommands(null);

        // act
        var result = sut.Parse(context, commandText);

        // assert
        result.Success.ShouldBeTrue();
        result.Command.ShouldBeOfType<TestCommand>()
            .Parser.Text.ShouldBe(commandText);
    }

    [Theory]
    [InlineData("group hello", true)]
    [InlineData("group  hello", true)]
    [InlineData("group goodbye", true)]
    [InlineData("bloop hello", false)]
    [InlineData("group ", false)]
    [InlineData("hello", false)]
    [InlineData("", false)]
    public void Parse_should_parse_prefix(string commandText, bool expectedSuccess)
    {
        // arrange
        var context = CreateContext(false);

        var sut = CreateSutWithTestCommands("group");

        // act
        var result = sut.Parse(context, commandText);

        // assert
        result.Success.ShouldBe(expectedSuccess);
    }

    [Theory]
    [InlineData("group hello", false, true)]
    [InlineData("GROUP hello", false, false)]
    [InlineData("Group hello", false, false)]
    [InlineData("group hello", true, true)]
    [InlineData("GROUP hello", true, true)]
    [InlineData("Group  hello", true, true)]
    [InlineData("bloop hello", true, false)]
    public void Parse_should_be_case_sensitive(string commandText, bool ignoreCase, bool expectedSuccess)
    {
        // arrange
        var context = CreateContext(ignoreCase);

        var sut = CreateSutWithTestCommands("group");

        // act
        var result = sut.Parse(context, commandText);
        
        // assert
        result.Success.ShouldBe(expectedSuccess);
    }

    private static CommandContext CreateContext(bool ignoreCase)
    {
        return new CommandContext(Mock.Of<ICommandProcessor>(), new CommandProcessorOptions { IgnoreCase = ignoreCase }, null);
    }

    private static CommandSubProcessor CreateSutWithTestCommands(string? prefix)
    {
        var sut = new CommandSubProcessor(prefix);
        sut.AddCommand(new TestCommandParser("hello", null));
        sut.AddCommand(new TestCommandParser("goodbye", null));

        return sut;
    }


    public interface ICommandParserAndCollection : ICommandParser, ICommandCollection
    {
    }
}