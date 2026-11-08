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
        ChatMessage chatMessage = new(ChatRole.User, $"What is the average weight of {catSpecies}?");
        ChatResponse response = await chatClient.GetResponseAsync(
            [chatMessage],
            options: new ChatOptions
            {
                Tools = [AIFunctionFactory.Create(CatAverageWeight.ByWeight)]
            },
            cancellationToken: cancellationToken);

        return response.Text;
    }

    public async Task<Step[]> CreatePlan(string taskDescription, CancellationToken cancellationToken = default)
    {
        ChatMessage systemPrompt = new(ChatRole.System, @"Zaplanuj kroki potrzebne, żeby odpowiedzieć na pytanie użytkownika.
Możliwe kroki:
- get_avg_weight <cat_species_name> - Zapytanie o średnią wagę podanego gatunku kota.
- sum - Zapytanie o sumę wyników poprzednich zapytań.

Odpowiedź zwróć jako listę nazw kroków i parametrów rozdzielonych przecinkami np.:

```
get_avg_weight,Siamese cat,get_avg_weight,European cat,sum
```

Nie zwracaj żadnych dodatkowych informacji, tylko listę kroków.");
        ChatMessage userMessage = new(ChatRole.User, taskDescription);
        ChatResponse response = await chatClient.GetResponseAsync(
            [systemPrompt, userMessage],
            cancellationToken: cancellationToken);
        return ParseSteps(response.Text);
    }

    private Step[] ParseSteps(string text)
    {
        List<Step> steps = [];
        string[] parts = text.Split(',');
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = parts[i].Trim();
            if (parts[i].StartsWith("get_avg_weight", StringComparison.OrdinalIgnoreCase))
            {
                steps.Add(new Step("get_avg_weight", parts[i + 1].Trim()));
            }
            else if (parts[i].Equals("sum", StringComparison.OrdinalIgnoreCase))
            {
                steps.Add(new Step("sum"));
            }
        }
        return steps.ToArray();
    }
}