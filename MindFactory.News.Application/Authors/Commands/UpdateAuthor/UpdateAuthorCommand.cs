using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using MindFactory.News.Application.Common.Responses;

namespace MindFactory.News.Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest<Result<SingleResponse>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static UpdateAuthorCommand CreateFrom(int Id, UpdateAuthorRequest request)
        {
            return new UpdateAuthorCommand
            {
                Id = Id,
                Name = request.Name
            };
        }
    }
}