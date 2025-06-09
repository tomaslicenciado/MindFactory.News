// <copyright file="GetNewsDetailsQuery.cs" company="PlaceholderCompany">
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

    public class GetNewsDetailsQuery : IRequest<Result<GetNewsDetailsResponse>>
    {
        public int Id { get; set; }

        public static GetNewsDetailsQuery CreateFrom(int id)
        {
            return new()
            {
                Id = id,
            };
        }
    }
}