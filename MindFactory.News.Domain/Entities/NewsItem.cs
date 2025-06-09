// <copyright file="NewsItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class NewsItem : Base.EntityAuditable
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public DateOnly PublishDate { get; set; }

        public string ImageUrl { get; set; }

        public int AuthorId { get; set; }

        public int? EditorialId { get; set; }

        [JsonIgnore]
        public virtual Author Author { get; set; }

        [JsonIgnore]
        public virtual Editorial? Editorial { get; set; } = null;
    }
}