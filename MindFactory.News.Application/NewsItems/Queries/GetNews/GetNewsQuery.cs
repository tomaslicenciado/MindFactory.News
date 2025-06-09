// <copyright file="GetNewsQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.NewsItems.Queries.GetNews
{
    using CSharpFunctionalExtensions;
    using MediatR;

    public class GetNewsQuery : IRequest<Result<GetNewsResponse>>
    {
        public DateOnly? PublishDate { get; set; }

        public string? TitleOrAuthor { get; set; }

        public static GetNewsQuery CreateFrom(DateOnly? publishDate, string? titleOrAuthor)
        {
            return new()
            {
                PublishDate = publishDate,
                TitleOrAuthor = titleOrAuthor,
            };
        }
    }
}