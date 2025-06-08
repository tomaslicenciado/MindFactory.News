using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MindFactory.News.Application.Interfaces;
using MindFactory.News.Domain.Entities;

namespace MindFactory.News.Application.Authors.Queries.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, Result<GetAuthorsResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAuthorsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GetAuthorsResponse>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = await _context.Authors
                    .Where(x => x.Enabled)
                    .Select(x => new AuthorData
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToListAsync(cancellationToken);

                return Result.Success(new GetAuthorsResponse
                {
                    Authors = authors
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