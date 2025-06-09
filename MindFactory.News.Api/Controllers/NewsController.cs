// <copyright file="NewsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Api.Controllers
{
    using System;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using MindFactory.News.Api.Extensions;
    using MindFactory.News.Application.NewsItems.Commands.AddNews;
    using MindFactory.News.Application.NewsItems.Commands.DeleteNews;
    using MindFactory.News.Application.NewsItems.Commands.UpdateNews;
    using MindFactory.News.Application.NewsItems.Queries.GetNews;
    using MindFactory.News.Application.NewsItems.Queries.GetNewsDetails;

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiVersion("1.0")]
    public class NewsController : ControllerBase
    {
        /// <summary>
        /// Create news.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="mediator">Mediator.</param>
        /// <returns>Message.</returns>
        [HttpPost("AddNews")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AddNews([FromBody] AddNewsRequest request, [FromServices] IMediator mediator)
        {
            var req = AddNewsCommand.CreateFrom(request);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }

        /// <summary>
        /// Update news.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="request">Request.</param>
        /// <param name="mediator">Mediator.</param>
        /// <returns>Message.</returns>
        [HttpPut("UpdateNews/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateNews(int id, [FromBody] UpdateNewsRequest request, [FromServices] IMediator mediator)
        {
            var req = UpdateNewsCommand.CreateFrom(id, request);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }

        /// <summary>
        /// Delete news.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="mediator">Mediator.</param>
        /// <returns>Message.</returns>
        [HttpDelete("DeleteNews/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteNews(int id, [FromServices] IMediator mediator)
        {
            var req = DeleteNewsCommand.CreateFrom(id);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }

        /// <summary>
        /// Get news list.
        /// </summary>
        /// <param name="titleOrAuthor">Title or Author.</param>
        /// <param name="publishDate">Publish Date.</param>
        /// <param name="mediator">Mediator.</param>
        /// <returns>List of news.</returns>
        [HttpGet("GetNews")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetNews(
            [FromQuery] string? titleOrAuthor,
            [FromQuery] DateOnly? publishDate,
            [FromServices] IMediator mediator)
        {
            var req = GetNewsQuery.CreateFrom(titleOrAuthor: titleOrAuthor, publishDate: publishDate);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }

        /// <summary>
        /// Get news details.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="mediator">Mediator.</param>
        /// <returns>Details of news.</returns>
        [HttpGet("GetNewsDetails/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetNewsDetails(int id, [FromServices] IMediator mediator)
        {
            var req = GetNewsDetailsQuery.CreateFrom(id);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }
    }
}