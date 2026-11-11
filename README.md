# Prompts library

A collection of prompts for the [Agent AI](https://github.com/lukasz-duda/agents-ai) solution.

Requirements:

- [Docker](https://docs.docker.com/engine/install/ubuntu/)
- [.NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

Start Ollama:

```bash
docker compose up -d
```

Pull model:

```bash
docker exec -it ollama ollama pull qwen3.5:4b
```

Test:

```bash
dotnet test
```
