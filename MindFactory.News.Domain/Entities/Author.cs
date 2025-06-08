using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MindFactory.News.Domain.Entities
{
    public class Author : Base.EntityAuditable
    {
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<NewsItem> NewsItems { get; set; } = new HashSet<NewsItem>();
    }
}