using NanoXLSX;
using Science.DB;
using Cell = NanoXLSX.Cell;

namespace Science.Features.DataLoader;

public static class ScopusParser
{
    public static async Task<List<ScopusData>> Parse(Stream fileStream)
    {
        var workBook = await Workbook.LoadAsync(fileStream);
        var workSheet = workBook.Worksheets[0];

        var data = new List<ScopusData>();

        for (var row = 1; row < workSheet.GetColumn(0).Count; row++)
        {
            var cells = workSheet.GetRow(row);

            data.Add(
                new ScopusData
                {
                    Authors = GetStringValue(cells, 0),
                    AuthorIDs = GetStringValue(cells, 1),
                    ArticleTitle = GetStringValue(cells, 2),
                    PublicationYear = GetStringValue(cells, 3),
                    SourceTitle = GetStringValue(cells, 4),
                    Volume = GetStringValue(cells, 5),
                    BookSeries = GetStringValue(cells, 6),
                    ArticleNumber = GetStringValue(cells, 7),
                    StartPage = GetStringValue(cells, 8),
                    EndPage = GetStringValue(cells, 9),
                    PageCount = GetStringValue(cells, 10),
                    Quotes = GetStringValue(cells, 11),
                    DOI = GetStringValue(cells, 12),
                    DOILink = GetStringValue(cells, 13),
                    Organization = GetStringValue(cells, 14),
                    OrganizationAuthors = GetStringValue(cells, 15),
                    Funding = GetStringValue(cells, 16),
                    FundingText =
                    [
                        GetStringValue(cells, 17),
                        GetStringValue(cells, 18),
                        GetStringValue(cells, 19),
                        GetStringValue(cells, 20),
                        GetStringValue(cells, 21),
                        GetStringValue(cells, 22),
                        GetStringValue(cells, 23),
                        GetStringValue(cells, 24),
                        GetStringValue(cells, 25),
                        GetStringValue(cells, 26)
                    ],
                    DocumentType = GetStringValue(cells, 27),
                    PublicationStage = GetStringValue(cells, 28),
                    OpenAccess = GetStringValue(cells, 29),
                    Source = GetStringValue(cells, 30),
                    EID = GetStringValue(cells, 31),
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
