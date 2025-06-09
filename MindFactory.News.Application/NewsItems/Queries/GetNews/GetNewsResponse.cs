// <copyright file="GetNewsResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.NewsItems.Queries.GetNews
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetNewsResponse
    {
        public List<NewsData> News { get; set; }
    }

    public class NewsData
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string ImageUrl { get; set; }
    }
}