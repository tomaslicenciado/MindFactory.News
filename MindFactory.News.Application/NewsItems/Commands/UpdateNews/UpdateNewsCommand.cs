using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using MindFactory.News.Application.Common.Responses;

namespace MindFactory.News.Application.NewsItems.Commands.UpdateNews
{
    public class UpdateNewsCommand : IRequest<Result<SingleResponse>>
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public DateOnly PublishDate { get; set; }

        public static UpdateNewsCommand CreateFrom(int Id, UpdateNewsRequest request)
        {
            return new()
            {
                NewsId = Id,
                Title = request.Title,
                Body = request.Body,
                ImageUrl = request.ImageUrl,
                AuthorId = request.AuthorId!.Value,
                PublishDate = request.PublishDate
            };
        }
    }
}