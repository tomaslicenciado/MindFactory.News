// <copyright file="GetAuthorsQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Authors.Queries.GetAuthors
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MindFactory.News.Application.Interfaces;

    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, Result<GetAuthorsResponse>>
    {
        private readonly IApplicationDbContext context;

        public GetAuthorsQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<GetAuthorsResponse>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = await context.Authors
                    .Where(x => x.Enabled)
                    .Select(x => new AuthorData
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToListAsync(cancellationToken);

                return Result.Success(new GetAuthorsResponse
                {
                    Authors = authors,
                });
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return Result.Failure<GetAuthorsResponse>($"An error occurred while retrieving authors: {ex.Message}");
            }
        }
    }
}