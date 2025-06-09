// <copyright file="GetAuthorsQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Authors.Queries.GetAuthors
{
    using CSharpFunctionalExtensions;
    using MediatR;

    public class GetAuthorsQuery : IRequest<Result<GetAuthorsResponse>>
    {
    }
}