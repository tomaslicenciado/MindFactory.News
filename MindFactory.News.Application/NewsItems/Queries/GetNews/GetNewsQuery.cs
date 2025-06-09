using CSharpFunctionalExtensions;
using MediatR;

namespace MindFactory.News.Application.NewsItems.Queries.GetNews
{
    public class GetNewsQuery : IRequest<Result<GetNewsResponse>>
    {
        public DateOnly? PublishDate { get; set; }
        public string? TitleOrAuthor { get; set; }

        public static GetNewsQuery CreateFrom(DateOnly? publishDate, string? titleOrAuthor)
        {
            return new()
            {
                PublishDate = publishDate,
                TitleOrAuthor = titleOrAuthor
            };
        }
    }
}