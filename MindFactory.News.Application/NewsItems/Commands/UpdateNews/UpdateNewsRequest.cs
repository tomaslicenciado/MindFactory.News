// <copyright file="UpdateNewsRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.NewsItems.Commands.UpdateNews
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateNewsRequest
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string ImageUrl { get; set; }

        [Range(1, int.MaxValue)]
        public int? AuthorId { get; set; }

        public DateOnly PublishDate { get; set; }
    }
}