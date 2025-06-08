using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;

namespace MindFactory.News.Application.Authors.Queries.GetAuthors
{
    public class GetAuthorsQuery : IRequest<Result<GetAuthorsResponse>>
    {
    }
}