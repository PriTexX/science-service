using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Science.DB;

public sealed class Article
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    public required string Authors { get; set; }
    public required string BookAuthors { get; set; }
    public required string BookEditors { get; set; }
    public required string BookGroupAuthors { get; set; }
    public required string AuthorFullNames { get; set; }
    public required string BookAuthorFullNames { get; set; }
    public required string GroupAuthors { get; set; }
    public required string ArticleTitle { get; set; }
    public required string SourceTitle { get; set; }
    public required string BookSeriesTitle { get; set; }
    public required string BookSeriesSubtitle { get; set; }
    public required string Language { get; set; }
    public required string DocumentType { get; set; }
    public required string ConferenceTitle { get; set; }
    public required string ConferenceDate { get; set; }
    public required string ConferenceLocation { get; set; }
    public required string ConferenceSponsor { get; set; }
    public required string ConferenceHost { get; set; }
    public required string AuthorKeywords { get; set; }
    public required string KeywordsPlus { get; set; }
    public required string Abstract { get; set; }
    public required string Addresses { get; set; }
    public required string Affiliations { get; set; }
    public required string ReprintAddresses { get; set; }
    public required string EmailAddresses { get; set; }
    public required string ResearcherIds { get; set; }
    public required string ORCIDs { get; set; }
    public required string FundingOrgs { get; set; }
    public required string FundingNamePreferred { get; set; }
    public required string FundingText { get; set; }
    public required string CitedReferences { get; set; }
    public required int CitedReferenceCount { get; set; }
    public required int TimesCitedCore { get; set; }
    public required int TimesCitedAll { get; set; }
    public required int HalfYearDayUsageCount { get; set; }
    public required int Since2013UsageCount { get; set; }
    public required string Publisher { get; set; }
    public required string PublisherCity { get; set; }
    public required string PublisherAddress { get; set; }
    public required string ISSN { get; set; }
    public required string EISSN { get; set; }
    public required string ISBN { get; set; }
    public required string JournalAbbreviation { get; set; }
    public required string JournalISOAbbreviation { get; set; }
    public required DateTimeOffset PublicationDate { get; set; }
    public required int PublicationYear { get; set; }
    public required string Volume { get; set; }
    public required string Issue { get; set; }
    public required string PartNumber { get; set; }
    public required string Supplement { get; set; }
    public required string SpecialIssue { get; set; }
    public required string MeetingAbstract { get; set; }
    public required string StartPage { get; set; }
    public required string EndPage { get; set; }
    public required string ArticleNumber { get; set; }
    public required string DOI { get; set; }
    public required string DOILink { get; set; }
    public required string BookDOI { get; set; }
    public required DateOnly EarlyAccessDate { get; set; }
    public required int NumberOfPages { get; set; }
    public required string WosCategories { get; set; }
    public required string WoSIndex { get; set; }
    public required string ResearchAreas { get; set; }
    public required string IDSNumber { get; set; }
    public required string PubmedId { get; set; }
    public required string OpenAccessDesignations { get; set; }
    public required string HighlyCitedStatus { get; set; }
    public required string HotPaperStatus { get; set; }
    public required DateOnly DateOfExport { get; set; }
    public required string UniqueWoSId { get; set; }
    public required string WoSRecord { get; set; }
}
