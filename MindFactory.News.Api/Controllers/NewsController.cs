using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MindFactory.News.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiVersion("1.0")]
    public class NewsController : ControllerBase
    {
        /// <summary>
        /// Create news.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
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
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
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
        /// <param name="id"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
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
        /// <param name="titleOrAuthor"></param>
        /// <param name="publishDate"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
        [HttpGet("GetNews")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetNews([FromQuery] string? titleOrAuthor,
            [FromQuery] DateOnly? publishDate, [FromServices] IMediator mediator)
        {
            var req = GetNewsQuery.CreateFrom(titleOrAuthor: titleOrAuthor, publishDate: publishDate);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }

        /// <summary>
        /// Get news details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mediator"></param>
        /// <returns></returns>
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