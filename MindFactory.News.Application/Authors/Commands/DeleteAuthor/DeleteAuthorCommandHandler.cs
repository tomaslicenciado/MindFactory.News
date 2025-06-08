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

namespace MindFactory.News.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result<SingleResponse>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAuthorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
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
            var author = await _context.Authors
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

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

            _context.Authors.Update(author);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new SingleResponse
            {
                Message = "Author deleted successfully."
            });
        }
    }
}