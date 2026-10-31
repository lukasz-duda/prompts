namespace Prompts.Tests;

public class PromptLibraryTests
{
    private readonly PromptLibrary _sut = new();

    [Fact]
    public void CanUsePromptLibrary()
    {
        _sut.Execute();
    }
}