// <copyright file="GetNewsDetailsResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.NewsItems.Queries.GetNewsDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetNewsDetailsResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string ImageUrl { get; set; }

        public string AuthorName { get; set; }

        public DateOnly PublishDate { get; set; }
    }
}