using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MindFactory.News.Application.Common.Responses;
using MindFactory.News.Application.Interfaces;
using MindFactory.News.Domain.Entities;

namespace MindFactory.News.Application.NewsItems.Commands.UpdateNews
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, Result<SingleResponse>>
    {
        private readonly IApplicationDbContext _context;

        public UpdateNewsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<SingleResponse>> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                NewsItem item = new();

                return await GetNewsItem(request, cancellationToken)
                        .Tap(x => item = x)
                    .Bind(x => ValidateRequest(request, cancellationToken))
                    .Bind(() => UpdateNewsAsync(request, item, cancellationToken));
            }
            catch (Exception e)
            {
                return Result.Failure<SingleResponse>($"Error updating the news: {e.Message}");
            }
        }

        private async Task<Result<NewsItem>> GetNewsItem(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.NewsItems
                .SingleOrDefaultAsync(x => x.Id == request.NewsId, cancellationToken);

            if (item == null)
                return Result.Failure<NewsItem>("News not found");

            return Result.Success(item);
        }

        private async Task<Result> ValidateRequest(UpdateNewsCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.Title))
                return Result.Failure("Title must not be empty");

            if (string.IsNullOrWhiteSpace(request.Body))
                return Result.Failure("Body must not be empty");

            if (!await _context.Authors.AnyAsync(x => x.Id == request.AuthorId))
                return Result.Failure("Author not found");

            return Result.Success();
        }

        private async Task<Result<SingleResponse>> UpdateNewsAsync(UpdateNewsCommand request,
                    NewsItem newsItem,
                    CancellationToken cancellationToken)
        {
            newsItem.Title = request.Title;
            newsItem.Body = request.Body;
            newsItem.ImageUrl = request.ImageUrl;
            newsItem.AuthorId = request.AuthorId;
            newsItem.PublishDate = request.PublishDate;
            newsItem.UpdatedDateTime = DateTime.UtcNow;

            _context.NewsItems.Update(newsItem);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new SingleResponse() { Message = "News successfully created" });
        }
    }
}