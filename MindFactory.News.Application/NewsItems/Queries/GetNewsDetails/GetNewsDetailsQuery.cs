using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;

namespace MindFactory.News.Application.NewsItems.Queries.GetNewsDetails
{
    public class GetNewsDetailsQuery : IRequest<Result<GetNewsDetailsResponse>>
    {
        public int Id { get; set; }

        public static GetNewsDetailsQuery CreateFrom(int id)
        {
            return new()
            {
                Id = id
            };
        }
    }
}