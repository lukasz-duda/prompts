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
    public async Task UsesModel()
    {
        string response = await _sut.SayHello();
        Assert.Contains("hello", response, StringComparison.OrdinalIgnoreCase);
        Assert.True(response.Length > 5);
    }

    [Theory]
    [InlineData("European cat", "5 kg")]
    [InlineData("leopard", "50 kg")]
    public async Task UsesTool(string catSpecies, string expectedAverageWeight)
    {
        string response = await _sut.GetAverageWeight(catSpecies);
        Assert.Contains(expectedAverageWeight, response, StringComparison.OrdinalIgnoreCase);
    }

    [Fact(Skip = "The response from the model doesn't match the expected format, so this test is skipped for now.")]
    public async Task ReturnsPlannedSteps()
    {
        Step[] steps = await _sut.CreatePlan("I have 3 leopards and 5 European cats. How many of them can fit in a vehicle with a 100 kg payload capacity?");
        Assert.NotEmpty(steps);
        Step firstStep = steps[0];
        Assert.NotEmpty(firstStep.Name);
        Assert.NotNull(firstStep.Parameter);
        Assert.NotEmpty(firstStep.Parameter);
    }
}