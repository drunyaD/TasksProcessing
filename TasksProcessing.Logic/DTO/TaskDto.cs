using System.Text.Json.Serialization;
using TasksProcessing.DataAccess.EF.Entities;

namespace TasksProcessing.Logic.DTO;

public record TaskDto
{
    public int Id { get; init; }

    public string Description { get; init; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaskState State { get; init; }

    public int TransferCount { get; init; }

    public string? CurrentUser { get; init; }
}
