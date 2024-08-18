using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Science.DB;
using Science.Features.DataLoader.Contracts;

namespace Science.Features.DataLoader;

[ApiController]
[Route("data")]
public sealed class DataLoaderController : ControllerBase
{
    private readonly DataLoaderService _dataLoaderService;
    private readonly ApplicationContext _appContext;

    public DataLoaderController(DataLoaderService dataLoaderService, ApplicationContext appContext)
    {
        _dataLoaderService = dataLoaderService;
        _appContext = appContext;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile scopusFile, IFormFile wosFile)
    {
        if (!IsExcelFile(scopusFile) || !IsExcelFile(wosFile))
        {
            return new BadRequestObjectResult(
                new { error = "FileExt", details = "Supported file extension is xlsx" }
            );
        }

        var scopus = ScopusParser.Parse(scopusFile.OpenReadStream());
        var wos = WosParser.Parse(wosFile.OpenReadStream());

        await Task.WhenAll(scopus, wos);

        var changes = await _dataLoaderService.UploadAsync(scopus.Result, wos.Result);

        return Ok(changes);
    }

    [HttpPost("all")]
    public async Task<IActionResult> GetAll(GetAllRequest req)
    {
        var query = Sort.Apply(_appContext.Articles.Skip(req.Limit).Take(req.Offset), req.Sorts);

        var articles = await query.ToListAsync();

        var response = articles
            .Select(a => new ArticleResponse
            {
                Authors = a.Authors,
                ArticleTitle = a.ArticleTitle,
                DocumentType = a.DocumentType,
                IsScopus = a.IsScopus,
                SourceTitle = a.SourceTitle,
                DOI = a.DOI,
                IsWoS = a.IsWoS,
            })
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var article = await _appContext.Articles.FindAsync(id);

        if (article == null)
        {
            return NotFound();
        }

        return Ok(
            new ArticleResponse
            {
                Authors = article.Authors,
                ArticleTitle = article.ArticleTitle,
                DocumentType = article.DocumentType,
                IsScopus = article.IsScopus,
                SourceTitle = article.SourceTitle,
                DOI = article.DOI,
                IsWoS = article.IsWoS,
            }
        );
    }

    private static bool IsExcelFile(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);

        return fileExtension == ".xlsx";
    }
}
