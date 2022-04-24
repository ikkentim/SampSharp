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

using Moq;
using SampSharp.Core.CodePages;
using Shouldly;
using Xunit;

namespace SampSharp.Core.UnitTests;

public class EncodingGameModeBuilderExtensionsTests
{
    [Fact]
    public void UseEncodingCodePage_should_load_correct_codepage()
    {
        // arrange
        IGameModeRunner? activeRunner = null;
        var sut = new GameModeBuilder();
        sut.Use(Mock.Of<IGameModeProvider>());
        sut.AddBuildAction(next =>
        {
            var runner = next();
            ((HostedGameModeClient)runner).InitializeForTesting();

            return runner;
        });

        sut.AddRunAction((runner, next) =>
        {
            next(runner);
            activeRunner = runner;
        });

        // act
        sut.UseEncodingCodePage("cp865");
        sut.Run();

        // assert
        var client = activeRunner.ShouldBeAssignableTo<IGameModeClient>()!;
        var encoding = client.Encoding.ShouldBeOfType<CodePageEncoding>();

        encoding.CodePage.ShouldBe(865);
        encoding.ConversionTable['Θ']
            .ShouldBe((ushort)0xe9);
    }
}