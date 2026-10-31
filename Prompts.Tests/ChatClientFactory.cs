using Microsoft.Extensions.AI;
using OllamaSharp;

namespace Prompts.Tests;

public static class ChatClientFactory
{
    public static IChatClient CreateChatClient() => new OllamaApiClient("http://localhost:11434", "llama3.2:3b");
}