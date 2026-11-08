using Microsoft.Extensions.AI;

namespace Prompts;

public class PromptLibrary(IChatClient chatClient)
{
    public async Task<string> SayHello(CancellationToken cancellationToken = default)
    {
        ChatMessage chatMessage = new(ChatRole.User, "Hello");
        ChatResponse response = await chatClient.GetResponseAsync(
            [chatMessage],
            cancellationToken: cancellationToken);

        return response.Text;
    }

    public async Task<string> GetAverageWeight(string catSpecies, CancellationToken cancellationToken = default)
    {
        ChatMessage chatMessage = new(ChatRole.User, $"What's average weight of {catSpecies}?");
        ChatResponse response = await chatClient.GetResponseAsync(
            [chatMessage],
            options: new ChatOptions
            {
                Tools = [AIFunctionFactory.Create(CatAverageWeight.ByWeight)]
            },
            cancellationToken: cancellationToken);

        return response.Text;
    }
}