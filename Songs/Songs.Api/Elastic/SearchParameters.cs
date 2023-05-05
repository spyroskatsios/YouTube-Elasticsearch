namespace Songs.Api.Elastic;

public record SearchParameters(string? SearchText, string? Genre, int Skip, int Take);