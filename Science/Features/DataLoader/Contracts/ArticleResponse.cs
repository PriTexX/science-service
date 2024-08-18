namespace Science.Features.DataLoader.Contracts;

public sealed class ArticleResponse
{
    public required string DOI { get; set; }
    public required string Authors { get; set; }
    public required string ArticleTitle { get; set; }
    public required string SourceTitle { get; set; }
    public required string DocumentType { get; set; }

    public required bool IsWoS { get; set; }
    public required bool IsScopus { get; set; }
}
