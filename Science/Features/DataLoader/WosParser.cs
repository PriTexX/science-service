using NanoXLSX;
using Science.DB;

namespace Science.Features.DataLoader;

public static class WosParser
{
    public static async Task<List<WoSData>> Parse(Stream fileStream)
    {
        var workBook = await Workbook.LoadAsync(fileStream);
        var workSheet = workBook.Worksheets[0];

        var data = new List<WoSData>();

        for (var row = 1; row < workSheet.GetColumn(0).Count; row++)
        {
            var cells = workSheet.GetRow(row);

            data.Add(
                new WoSData
                {
                    PublicationType = GetStringValue(cells, 0),
                    Authors = GetStringValue(cells, 1),
                    BookAuthors = GetStringValue(cells, 2),
                    BookEditors = GetStringValue(cells, 3),
                    BookGroupAuthors = GetStringValue(cells, 4),
                    AuthorFullNames = GetStringValue(cells, 5),
                    BookAuthorFullNames = GetStringValue(cells, 6),
                    GroupAuthors = GetStringValue(cells, 7),
                    ArticleTitle = GetStringValue(cells, 8),
                    SourceTitle = GetStringValue(cells, 9),
                    BookSeriesTitle = GetStringValue(cells, 10),
                    BookSeriesSubtitle = GetStringValue(cells, 11),
                    Language = GetStringValue(cells, 12),
                    DocumentType = GetStringValue(cells, 13),
                    ConferenceTitle = GetStringValue(cells, 14),
                    ConferenceDate = GetStringValue(cells, 15),
                    ConferenceLocation = GetStringValue(cells, 16),
                    ConferenceSponsor = GetStringValue(cells, 17),
                    ConferenceHost = GetStringValue(cells, 18),
                    AuthorKeywords = GetStringValue(cells, 19),
                    KeywordsPlus = GetStringValue(cells, 20),
                    Abstract = GetStringValue(cells, 21),
                    Addresses = GetStringValue(cells, 22),
                    Affiliations = GetStringValue(cells, 23),
                    ReprintAddresses = GetStringValue(cells, 24),
                    EmailAddresses = GetStringValue(cells, 25),
                    ResearcherIds = GetStringValue(cells, 26),
                    ORCIDs = GetStringValue(cells, 27),
                    FundingOrgs = GetStringValue(cells, 28),
                    FundingNamePreferred = GetStringValue(cells, 29),
                    FundingText = GetStringValue(cells, 30),
                    CitedReferences = GetStringValue(cells, 31),
                    CitedReferenceCount = GetStringValue(cells, 32),
                    TimesCitedCore = GetStringValue(cells, 33),
                    TimesCitedAll = GetStringValue(cells, 34),
                    HalfYearDayUsageCount = GetStringValue(cells, 35),
                    Since2013UsageCount = GetStringValue(cells, 36),
                    Publisher = GetStringValue(cells, 37),
                    PublisherCity = GetStringValue(cells, 38),
                    PublisherAddress = GetStringValue(cells, 39),
                    ISSN = GetStringValue(cells, 40),
                    EISSN = GetStringValue(cells, 41),
                    ISBN = GetStringValue(cells, 42),
                    JournalAbbreviation = GetStringValue(cells, 43),
                    JournalISOAbbreviation = GetStringValue(cells, 44),
                    PublicationDate = GetStringValue(cells, 45),
                    PublicationYear = GetStringValue(cells, 46),
                    Volume = GetStringValue(cells, 47),
                    Issue = GetStringValue(cells, 48),
                    PartNumber = GetStringValue(cells, 49),
                    Supplement = GetStringValue(cells, 50),
                    SpecialIssue = GetStringValue(cells, 51),
                    MeetingAbstract = GetStringValue(cells, 52),
                    StartPage = GetStringValue(cells, 53),
                    EndPage = GetStringValue(cells, 54),
                    ArticleNumber = GetStringValue(cells, 55),
                    DOI = GetStringValue(cells, 56),
                    DOILink = GetStringValue(cells, 57),
                    BookDOI = GetStringValue(cells, 58),
                    EarlyAccessDate = GetStringValue(cells, 59),
                    NumberOfPages = GetStringValue(cells, 60),
                    WosCategories = GetStringValue(cells, 61),
                    WoSIndex = GetStringValue(cells, 62),
                    ResearchAreas = GetStringValue(cells, 63),
                    IDSNumber = GetStringValue(cells, 64),
                    PubmedId = GetStringValue(cells, 65),
                    OpenAccessDesignations = GetStringValue(cells, 66),
                    HighlyCitedStatus = GetStringValue(cells, 67),
                    HotPaperStatus = GetStringValue(cells, 68),
                    DateOfExport = GetStringValue(cells, 69),
                    UniqueWoSId = GetStringValue(cells, 70),
                    WoSRecord = GetStringValue(cells, 71),
                }
            );
        }

        return data;
    }

    private static string GetStringValue(IEnumerable<Cell> cells, int column)
    {
        var value = cells.FirstOrDefault(c => c.ColumnNumber == column);

        return value?.Value.ToString() ?? "";
    }
}
