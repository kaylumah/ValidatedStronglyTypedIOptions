// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Xunit;

namespace Test.Unit;

public sealed class TestProjectFixture
{
}

[CollectionDefinition(Name)]
public sealed class TestProjectCollection : ICollectionFixture<TestProjectFixture>
{
    public const string Name = "Test.Unit";
}
