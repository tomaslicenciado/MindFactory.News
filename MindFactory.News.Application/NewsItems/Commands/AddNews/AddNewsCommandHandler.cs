// <copyright file="AddNewsCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.NewsItems.Commands.AddNews
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MindFactory.News.Application.Common.Responses;
    using MindFactory.News.Application.Interfaces;
    using MindFactory.News.Domain.Entities;

    public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand, Result<SingleResponse>>
    {
        private readonly IApplicationDbContext context;

        public AddNewsCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
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
            {
                return Result.Failure("Title must not be empty");
            }

            if (string.IsNullOrWhiteSpace(request.Body))
            {
                return Result.Failure("Body must not be empty");
            }

            if (!await context.Authors.AnyAsync(x => x.Id == request.AuthorId, cancellationToken))
            {
                return Result.Failure("Author not found");
            }

            return Result.Success();
        }

        private async Task<Result<SingleResponse>> SaveNewsAsync(AddNewsCommand request, CancellationToken cancellationToken)
        {
            context.NewsItems.Add(new NewsItem()
            {
                Title = request.Title,
                Body = request.Body,
                PublishDate = request.PublishDate,
                ImageUrl = request.ImageUrl,
                AuthorId = request.AuthorId,
            });

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(new SingleResponse() { Message = "News successfully created" });
        }
    }
}