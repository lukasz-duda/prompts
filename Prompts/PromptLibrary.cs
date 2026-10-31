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
}