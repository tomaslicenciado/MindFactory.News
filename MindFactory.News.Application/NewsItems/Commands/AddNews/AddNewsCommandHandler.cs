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

namespace MindFactory.News.Application.NewsItems.Commands.AddNews
{
    public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand, Result<SingleResponse>>
    {
        private readonly IApplicationDbContext _context;

        public AddNewsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<SingleResponse>> Handle(AddNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await ValidateRequest(request, cancellationToken)
                    .Bind(() => SaveNewsAsync(request, cancellationToken));
            }
            catch (Exception e)
            {
                return Result.Failure<SingleResponse>($"Error adding the news: {e.Message}");
            }
        }

        private async Task<Result> ValidateRequest(AddNewsCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return Result.Failure("Title must not be empty");

            if (string.IsNullOrWhiteSpace(request.Body))
                return Result.Failure("Body must not be empty");

            if (!await _context.Authors.AnyAsync(x => x.Id == request.AuthorId))
                return Result.Failure("Author not found");

            return Result.Success();
        }

        private async Task<Result<SingleResponse>> SaveNewsAsync(AddNewsCommand request, CancellationToken cancellationToken)
        {
            _context.NewsItems.Add(new NewsItem()
            {
                Title = request.Title,
                Body = request.Body,
                PublishDate = request.PublishDate,
                ImageUrl = request.ImageUrl,
                AuthorId = request.AuthorId
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new SingleResponse() { Message = "News successfully created" });
        }
    }
}