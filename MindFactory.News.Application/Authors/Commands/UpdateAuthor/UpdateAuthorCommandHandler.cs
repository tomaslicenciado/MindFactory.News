// <copyright file="UpdateAuthorCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Authors.Commands.UpdateAuthor
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MindFactory.News.Application.Common.Responses;
    using MindFactory.News.Application.Interfaces;
    using MindFactory.News.Domain.Entities;

    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result<SingleResponse>>
    {
        private readonly IApplicationDbContext context;

        public UpdateAuthorCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<SingleResponse>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Author author = new();
                return await GetAuthor(request, cancellationToken)
                        .Tap(x => author = x)
                    .Bind(author => ValidateRequest(request))
                    .Bind(() => UpdateAuthor(request, author, cancellationToken));
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return Result.Failure<SingleResponse>($"An error occurred while updating the author: {ex.Message}");
            }
        }

        private async Task<Result<Author>> GetAuthor(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await context.Authors
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (author == null)
            {
                return Result.Failure<Author>($"Author with ID {request.Id} not found.");
            }

            return Result.Success(author!);
        }

        private Result ValidateRequest(UpdateAuthorCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Result.Failure("Author name cannot be empty.");
            }

            return Result.Success();
        }

        private async Task<Result<SingleResponse>> UpdateAuthor(
            UpdateAuthorCommand request,
            Author author,
            CancellationToken cancellationToken)
        {
            author.Name = request.Name;
            author.UpdatedDateTime = DateTime.UtcNow;

            context.Authors.Update(author);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(new SingleResponse { Message = "Author updated successfully." });
        }
    }
}