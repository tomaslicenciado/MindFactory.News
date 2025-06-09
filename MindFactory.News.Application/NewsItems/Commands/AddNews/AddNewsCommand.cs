// <copyright file="AddNewsCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.NewsItems.Commands.AddNews
{
    using System;
    using CSharpFunctionalExtensions;
    using MediatR;
    using MindFactory.News.Application.Common.Responses;

    public class AddNewsCommand : IRequest<Result<SingleResponse>>
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string ImageUrl { get; set; }

        public int AuthorId { get; set; }

        public DateOnly PublishDate { get; set; }

        public static AddNewsCommand CreateFrom(AddNewsRequest request)
        {
            return new()
            {
                Title = request.Title,
                Body = request.Body,
                ImageUrl = request.ImageUrl,
                PublishDate = request.PublishDate,
                AuthorId = request.AuthorId!.Value,
            };
        }
    }
}