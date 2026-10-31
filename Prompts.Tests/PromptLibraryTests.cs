using Microsoft.Extensions.AI;

namespace Prompts.Tests;

public class PromptLibraryTests
{
    [Fact]
    public async Task CanUsePromptLibrary()
    {
        IChatClient chatClient = ChatClientFactory.CreateChatClient();
        var sut = new PromptLibrary(chatClient);
        string response = await sut.SayHello();
        Assert.NotEmpty(response);
    }
}