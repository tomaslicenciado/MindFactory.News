// <copyright file="AddAuthorCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Authors.Commands.AddAuthor
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using MindFactory.News.Application.Common.Responses;

    public class AddAuthorCommand : IRequest<Result<SingleResponse>>
    {
        public string Name { get; set; }

        public static AddAuthorCommand CreateFrom(AddAuthorRequest request)
        {
            return new AddAuthorCommand
            {
                Name = request.Name,
            };
        }
    }
}