using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MindFactory.News.Application.Common.Responses;
using MindFactory.News.Application.Interfaces;
using MindFactory.News.Domain.Entities;

namespace MindFactory.News.Application.NewsItems.Commands.DeleteNews
{
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, Result<SingleResponse>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteNewsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<SingleResponse>> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await GetNews(request, cancellationToken)
                    .Bind(x => DeleteNewsAsync(x, cancellationToken));
            }
            catch (Exception e)
            {
                return Result.Failure<SingleResponse>($"Error deleting news: {e.Message}");
            }
        }

        private async Task<Result<NewsItem>> GetNews(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _context.NewsItems
                .SingleOrDefaultAsync(x => x.Id == request.Id && x.Enabled, cancellationToken);

            if (news == null)
                return Result.Failure<NewsItem>("News not found");

            return Result.Success(news);
        }

        private async Task<Result<SingleResponse>> DeleteNewsAsync(NewsItem news, CancellationToken cancellationToken)
        {
            news.Enabled = false;
            news.UpdatedDateTime = DateTime.UtcNow;

            _context.NewsItems.Update(news);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new SingleResponse() {Message = "News successfully deleted"});
        }
    }
}