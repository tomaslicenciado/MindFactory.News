using System.ComponentModel.DataAnnotations;

namespace MindFactory.News.Application.NewsItems.Commands.UpdateNews
{
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