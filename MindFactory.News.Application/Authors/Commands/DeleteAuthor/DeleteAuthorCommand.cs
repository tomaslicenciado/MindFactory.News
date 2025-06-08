using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using MindFactory.News.Application.Common.Responses;

namespace MindFactory.News.Application.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest<Result<SingleResponse>>
    {
        public int Id { get; set; }

        public static DeleteAuthorCommand CreateFrom(int id)
        {
            return new DeleteAuthorCommand
            {
                Id = id
            };
        }       
    }
}