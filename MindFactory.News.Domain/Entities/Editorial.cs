using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MindFactory.News.Domain.Entities
{
    public class Editorial : Base.EntityAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly PublishDate { get; set; }
        [JsonIgnore]
        public virtual ICollection<NewsItem> NewsItems { get; set; } = new HashSet<NewsItem>();
    }
}