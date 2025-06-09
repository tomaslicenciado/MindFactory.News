// <copyright file="AddAuthorCommandHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Authors.Commands.AddAuthor
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using MindFactory.News.Application.Common.Responses;
    using MindFactory.News.Application.Interfaces;
    using MindFactory.News.Domain.Entities;

    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Result<SingleResponse>>
    {
        private readonly IApplicationDbContext context;

        public AddAuthorCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<SingleResponse>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await ValidateRequest(request)
                    .Bind(() => SaveAuthor(request, cancellationToken));
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return Result.Failure<SingleResponse>($"An error occurred while adding the author: {ex.Message}");
            }
        }

        private static Result ValidateRequest(AddAuthorCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Result.Failure("Author name cannot be empty.");
            }

            return Result.Success();
        }

        private async Task<Result<SingleResponse>> SaveAuthor(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            context.Authors.Add(new Author
                {
                    Name = request.Name,
                });

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(new SingleResponse
            {
                Message = "Author added successfully.",
            });
        }
    }
}