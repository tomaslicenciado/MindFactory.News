// <copyright file="DeleteAuthorCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Authors.Commands.DeleteAuthor
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using MindFactory.News.Application.Common.Responses;

    public class DeleteAuthorCommand : IRequest<Result<SingleResponse>>
    {
        public int Id { get; set; }

        public static DeleteAuthorCommand CreateFrom(int id)
        {
            return new DeleteAuthorCommand
            {
                Id = id,
            };
        }
    }
}