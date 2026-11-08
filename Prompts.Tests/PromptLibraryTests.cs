using Microsoft.Extensions.AI;
using OllamaSharp;

namespace Prompts.Tests;

public class PromptLibraryTests
{
    private IChatClient _chatClient;
    private PromptLibrary _sut;

    public PromptLibraryTests()
    {
        _chatClient = ChatClientFactory.CreateChatClient();
        _sut = new PromptLibrary(_chatClient);
    }

    [Fact]
    public async Task CanUsePromptLibrary()
    {
        string response = await _sut.SayHello();
        Assert.Contains("hello", response, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("European cat", "5 kg")]
    [InlineData("leopard", "50 kg")]
    public async Task CanUseTool(string catSpecies, string expectedAverageWeight)
    {
        string response = await _sut.GetAverageWeight(catSpecies);
        Assert.Contains(expectedAverageWeight, response, StringComparison.OrdinalIgnoreCase);
    }
}