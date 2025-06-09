// <copyright file="DeleteAuthorCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Authors.Commands.DeleteAuthor
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MindFactory.News.Application.Common.Responses;
    using MindFactory.News.Application.Interfaces;
    using MindFactory.News.Domain.Entities;

    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result<SingleResponse>>
    {
        private readonly IApplicationDbContext context;

        public DeleteAuthorCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<SingleResponse>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await GetAuthor(request, cancellationToken)
                    .Bind(author => DeleteAuthor(author, cancellationToken));
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return Result.Failure<SingleResponse>($"An error occurred while deleting the author: {ex.Message}");
            }
        }

        private async Task<Result<Author>> GetAuthor(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await context.Authors
                .SingleOrDefaultAsync(x => x.Id == request.Id && x.Enabled, cancellationToken);

            if (author == null)
            {
                return Result.Failure<Author>($"Author with ID {request.Id} not found.");
            }

            return Result.Success(author);
        }

        private async Task<Result<SingleResponse>> DeleteAuthor(Author author, CancellationToken cancellationToken)
        {
            author.Enabled = false;
            author.UpdatedDateTime = DateTime.UtcNow;

            context.Authors.Update(author);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(new SingleResponse
            {
                Message = "Author deleted successfully.",
            });
        }
    }
}