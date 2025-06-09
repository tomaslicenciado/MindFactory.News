// <copyright file="UpdateAuthorCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Authors.Commands.UpdateAuthor
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using MindFactory.News.Application.Common.Responses;

    public class UpdateAuthorCommand : IRequest<Result<SingleResponse>>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static UpdateAuthorCommand CreateFrom(int id, UpdateAuthorRequest request)
        {
            return new UpdateAuthorCommand
            {
                Id = id,
                Name = request.Name,
            };
        }
    }
}