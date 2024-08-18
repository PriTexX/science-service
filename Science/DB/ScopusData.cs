namespace Science.DB;

public sealed class ScopusData
{
    public required string Authors { get; set; }
    public required string AuthorIDs { get; set; }
    public required string ArticleTitle { get; set; }
    public required string PublicationYear { get; set; }
    public required string SourceTitle { get; set; }
    public required string Volume { get; set; }
    public required string BookSeries { get; set; }
    public required string ArticleNumber { get; set; }
    public required string StartPage { get; set; }
    public required string EndPage { get; set; }
    public required string PageCount { get; set; }
    public required string Quotes { get; set; } // Цитирования
    public required string DOI { get; set; }
    public required string DOILink { get; set; } // Ссылка
    public required string Organization { get; set; }
    public required string OrganizationAuthors { get; set; }

    // public required string ShortDescription { get; set; }
    // public required string AuthorKeywords { get; set; }
    // public required string KeywordsPlus { get; set; } // скорее всего одно и то же с Ключевыми словами указателя
    // public required string MolecularSequenceNumber { get; set; }
    // public required string CAS { get; set; } // Хим. вещества
    // public required string BrandNames { get; set; }
    // public required string Producers { get; set; }
    public required string Funding { get; set; } // Сведения о финансировании
    public required List<string> FundingText { get; set; } // Текст о финансировании

    // public required string ArticleLinks { get; set; } // Пристатейные ссылки
    // public required string ReprintAddress { get; set; } // Адрес для корреспонденции
    // public required string Editors { get; set; }
    // public required string Sponsors { get; set; }
    // public required string Publisher { get; set; }
    // public required string ConferenceName { get; set; }
    // public required DateOnly ConferenceDate { get; set; }
    // public required string ConferencePlace { get; set; }
    // public required string ConferenceCode { get; set; }
    // public required string ISSN { get; set; }
    // public required string ISBN { get; set; }
    // public required string CODEN { get; set; }
    // public required string PubmedId { get; set; }
    // public required string Language { get; set; }
    // public required string SourceShort { get; set; }
    public required string DocumentType { get; set; }
    public required string PublicationStage { get; set; }
    public required string OpenAccess { get; set; }
    public required string Source { get; set; }
    public required string EID { get; set; }
}
