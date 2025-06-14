// <copyright file="GetNewsDetailsQueryHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.NewsItems.Queries.GetNewsDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MindFactory.News.Application.Interfaces;

    public class GetNewsDetailsQueryHandler : IRequestHandler<GetNewsDetailsQuery, Result<GetNewsDetailsResponse>>
    {
        private readonly IApplicationDbContext context;

        public GetNewsDetailsQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<GetNewsDetailsResponse>> Handle(GetNewsDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var details = await context.NewsItems.AsNoTracking()
                    .Where(x => x.Enabled && x.Id == request.Id)
                    .Select(x => new GetNewsDetailsResponse()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ImageUrl = x.ImageUrl,
                        PublishDate = x.PublishDate,
                        AuthorName = x.Author.Name,
                        Body = x.Body,
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (details == null)
                {
                    return Result.Failure<GetNewsDetailsResponse>("News not found");
                }

                return Result.Success(details);
            }
            catch (Exception e)
            {
                return Result.Failure<GetNewsDetailsResponse>($"Error getting news details: {e.Message}");
            }
        }
    }
}