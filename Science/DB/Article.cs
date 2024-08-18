using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Science.DB;

public sealed class Article
{
    [Key]
    [MaxLength(26)]
    public required string Id { get; set; }

    public required string DOI { get; set; }
    public required string Authors { get; set; }
    public required string ArticleTitle { get; set; }
    public required string SourceTitle { get; set; }
    public required string DocumentType { get; set; }
    public required string PublicationYear { get; set; }

    public bool IsWoS { get; set; }
    public bool IsScopus { get; set; }

    public string? WoSId { get; set; }
    public string? ScopusId { get; set; }

    [Column(TypeName = "jsonb")]
    public WoSData? WoSData { get; set; }

    [Column(TypeName = "jsonb")]
    public ScopusData? ScopusData { get; set; }
}
