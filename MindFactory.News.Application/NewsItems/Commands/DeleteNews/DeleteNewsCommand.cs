// <copyright file="DeleteNewsCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.NewsItems.Commands.DeleteNews
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using MindFactory.News.Application.Common.Responses;

    public class DeleteNewsCommand : IRequest<Result<SingleResponse>>
    {
        public int Id { get; set; }

        public static DeleteNewsCommand CreateFrom(int id)
        {
            return new()
            {
                Id = id,
            };
        }
    }
}