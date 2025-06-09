using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MindFactory.News.Application.NewsItems.Commands.AddNews
{
    public class AddNewsRequest
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string ImageUrl { get; set; }

        [Range(1, int.MaxValue)]
        public int? AuthorId { get; set; }

        public DateOnly PublishDate { get; set; }
    }
}