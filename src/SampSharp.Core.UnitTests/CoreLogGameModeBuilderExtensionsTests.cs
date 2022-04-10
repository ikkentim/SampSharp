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

using System.IO;
using System.Text;
using Moq;
using SampSharp.Core.Logging;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests;

public class CoreLogGameModeBuilderExtensionsTests
{
    [Fact]
    public void UseLogLevel_should_set_CoreLog_LogLevel()
    {
        // arrange
        var sut = new GameModeBuilder();
        sut.AddBuildAction(_ => Mock.Of<IGameModeRunner>());

        // act
        sut.UseLogLevel(CoreLogLevel.Error);
        sut.Run();
        
        // assert
        CoreLog.LogLevel.ShouldBe(CoreLogLevel.Error);
    }

    [Fact]
    public void UseLogWriter_should_set_CoreLog_TextWriter()
    {
        // arrange
        var writer = Mock.Of<TextWriter>();

        var sut = new GameModeBuilder();
        sut.AddBuildAction(_ => Mock.Of<IGameModeRunner>());

        // act
        sut.UseLogWriter(writer);
        sut.Run();
        
        // assert
        CoreLog.TextWriter.ShouldBe(writer);
    }
}