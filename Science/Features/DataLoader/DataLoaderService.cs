using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Science.DB;

namespace Science.Features.DataLoader;

using ArticleChanges = Dictionary<string, Dictionary<string, ArticlePropsChanges>>;

public sealed class ArticlePropsChanges
{
    public required object? Old { get; init; }
    public required object? New { get; init; }
}

public sealed class DataLoaderService
{
    private readonly ApplicationContext _appContext;

    public DataLoaderService(ApplicationContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<ArticleChanges> UploadAsync(List<ScopusData> scopus, List<WoSData> wos)
    {
        var wosCopy = new List<WoSData>(wos);
        var changes = new ArticleChanges();

        foreach (var scopusData in scopus)
        {
            var wosData = FindWoSData(scopusData, wosCopy);

            var dbArticle = await _appContext.Articles.FirstOrDefaultAsync(a =>
                a.ScopusId == scopusData.EID
            );

            var article = new Article
            {
                Id = Ulid.NewUlid().ToString(),

                DOI = scopusData.DOI,
                Authors = scopusData.Authors,
                ArticleTitle = scopusData.ArticleTitle,
                SourceTitle = scopusData.SourceTitle,
                DocumentType = scopusData.DocumentType,
                PublicationYear = scopusData.PublicationYear,

                IsScopus = true,
                IsWoS = wosData != null,

                ScopusId = scopusData.EID,
                WoSId = wosData?.UniqueWoSId,

                ScopusData = scopusData,
                WoSData = wosData
            };

            await CompareAndUpdate(article, dbArticle, changes);

            if (wosData != null)
            {
                wosCopy.Remove(wosData);
            }
        }

        foreach (var wosData in wosCopy)
        {
            var article = new Article
            {
                Id = Ulid.NewUlid().ToString(),

                DOI = wosData.DOI,
                Authors = wosData.Authors,
                ArticleTitle = wosData.ArticleTitle,
                SourceTitle = wosData.SourceTitle,
                DocumentType = wosData.DocumentType,
                PublicationYear = wosData.PublicationYear,

                IsScopus = false,
                IsWoS = true,

                ScopusId = null,
                WoSId = wosData.UniqueWoSId,

                ScopusData = null,
                WoSData = wosData
            };

            var dbArticle = await _appContext.Articles.FirstOrDefaultAsync(a =>
                a.WoSId == wosData.UniqueWoSId
            );

            await CompareAndUpdate(article, dbArticle, changes);
        }

        return changes;
    }

    private async Task CompareAndUpdate(Article article, Article? dbArticle, ArticleChanges changes)
    {
        if (dbArticle == null)
        {
            _appContext.Articles.Add(article);
            await _appContext.SaveChangesAsync();
        }
        else
        {
            article.Id = dbArticle.Id;

            var serializedDbArticle = JsonSerializer.Serialize(dbArticle);
            var serializedArticle = JsonSerializer.Serialize(article);

            if (serializedDbArticle != serializedArticle)
            {
                var propsChanges = new Dictionary<string, ArticlePropsChanges>();

                if (article.WoSData?.DOI != dbArticle.WoSData?.DOI)
                {
                    propsChanges.Add(
                        "WoSData__DOI",
                        new ArticlePropsChanges
                        {
                            Old = dbArticle.WoSData?.DOI,
                            New = article.WoSData?.DOI
                        }
                    );
                }

                if (article.ScopusData?.DOI != dbArticle.ScopusData?.DOI)
                {
                    propsChanges.Add(
                        "ScopusData__DOI",
                        new ArticlePropsChanges
                        {
                            Old = dbArticle.ScopusData?.DOI,
                            New = article.ScopusData?.DOI
                        }
                    );
                }

                if (propsChanges.Count > 0)
                {
                    changes.Add(article.Id, propsChanges);
                }

                _appContext.Entry(dbArticle).CurrentValues.SetValues(article);
                await _appContext.SaveChangesAsync();
            }
        }
    }

    private static WoSData? FindWoSData(ScopusData scopus, List<WoSData> wos)
    {
        var wosData = wos.FirstOrDefault(w =>
            !string.IsNullOrEmpty(scopus.DOI) && w.DOI == scopus.DOI
        );

        if (wosData != null)
        {
            return wosData;
        }

        return wos.FirstOrDefault(w =>
            w.ArticleTitle == scopus.ArticleTitle && w.SourceTitle == scopus.SourceTitle
        );
    }
}
