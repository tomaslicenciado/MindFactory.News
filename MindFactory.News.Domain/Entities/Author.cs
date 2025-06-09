// <copyright file="Author.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class Author : Base.EntityAuditable
    {
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<NewsItem> NewsItems { get; set; } = new HashSet<NewsItem>();
    }
}