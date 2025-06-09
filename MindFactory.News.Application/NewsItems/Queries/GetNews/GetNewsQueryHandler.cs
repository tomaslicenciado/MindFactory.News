
using System.Globalization;
using System.Text;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MindFactory.News.Application.Interfaces;
using MindFactory.News.Domain.Entities;
using NpgsqlTypes;

namespace MindFactory.News.Application.NewsItems.Queries.GetNews
{
    public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, Result<GetNewsResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetNewsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GetNewsResponse>> Handle(GetNewsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await GetQuery(request)
                    .Bind(x => RetrieveNewsAsync(x, cancellationToken));
            }
            catch (Exception e)
            {
                return Result.Failure<GetNewsResponse>($"Error getting all news: {e.Message}");
            }
        }

        private Result<IQueryable<NewsItem>> GetQuery(GetNewsQuery request)
        {
            var itemsSet = _context.NewsItems;

            var query = itemsSet.AsNoTracking();

            if (request.TitleOrAuthor != null)
                query = itemsSet
                   .FromSqlInterpolated($@"
                        SELECT * FROM ""NewsItems""
                        WHERE ""SearchVector"" @@ plainto_tsquery('spanish', {request.TitleOrAuthor})")
                    .AsNoTracking();

            if (request.PublishDate.HasValue)
                query = query.Where(x => x.PublishDate == request.PublishDate.Value);

            return Result.Success(_context.NewsItems.AsNoTracking()
                .Where(x => x.Enabled));
        }

        private async Task<Result<GetNewsResponse>> RetrieveNewsAsync(IQueryable<NewsItem> query, CancellationToken cancellationToken)
        {
            var news = await query
                .Select(x => new NewsData()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    AuthorName = x.Author.Name
                })
                .ToListAsync(cancellationToken);

            return Result.Success(new GetNewsResponse()
            {
                News = news
            });
        }

        private static bool StringContains(string stringBase, string stringBuscado)
        {
            var strings = stringBuscado.Split(' ');
            return strings.All(s => RemoveDiacritics(stringBase.ToLower())
                .Contains(RemoveDiacritics(s.ToLower())));
        }
        

        private static string RemoveDiacritics(string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                    .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            ).Normalize(NormalizationForm.FormC)
            .Replace("á", "a")
            .Replace("é", "e")
            .Replace("í", "i")
            .Replace("ó", "o")
            .Replace("ú", "u");
        }
    }
}