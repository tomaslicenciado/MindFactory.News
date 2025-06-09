using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using MindFactory.News.Application.Common.Responses;

namespace MindFactory.News.Application.NewsItems.Commands.AddNews
{
    public class AddNewsCommand : IRequest<Result<SingleResponse>>
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string ImageUrl { get; set; }

        public int AuthorId { get; set; }

        public DateOnly PublishDate { get; set; }

        public static AddNewsCommand CreateFrom(AddNewsRequest request)
        {
            return new()
            {
                Title = request.Title,
                Body = request.Body,
                ImageUrl = request.ImageUrl,
                PublishDate = request.PublishDate,
                AuthorId = request.AuthorId!.Value
            };
        }
    }
}