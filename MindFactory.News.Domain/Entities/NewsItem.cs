using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MindFactory.News.Domain.Entities
{
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