// <copyright file="AuthorsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Api.Controllers
{
    using System.Net.Mime;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using MindFactory.News.Api.Extensions;
    using MindFactory.News.Application.Authors.Commands.AddAuthor;
    using MindFactory.News.Application.Authors.Commands.DeleteAuthor;
    using MindFactory.News.Application.Authors.Commands.UpdateAuthor;
    using MindFactory.News.Application.Authors.Queries.GetAuthors;

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiVersion("1.0")]
    public class AuthorsController : ControllerBase
    {
        /// <summary>
        /// Create new Author.
        /// </summary>
        /// <param name="request">Request.</param>
        /// <param name="mediator">Mediator.</param>
        /// <returns>Message.</returns>
        [HttpPost("AddAuthor")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest request, [FromServices] IMediator mediator)
        {
            var req = AddAuthorCommand.CreateFrom(request);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }

        /// <summary>
        /// Update a specified Author.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="request">Request.</param>
        /// <param name="mediator">Mediator.</param>
        /// <returns>Message.</returns>
        [HttpPut("UpdateAuthor/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorRequest request, [FromServices] IMediator mediator)
        {
            var req = UpdateAuthorCommand.CreateFrom(id, request);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }

        /// <summary>
        /// Delete a specified Author.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="mediator">Mediator.</param>
        /// <returns>Message.</returns>
        [HttpDelete("DeleteAuthor/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteAuthor(int id, [FromServices] IMediator mediator)
        {
            var req = DeleteAuthorCommand.CreateFrom(id);
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }

        /// <summary>
        /// Get the list of the aothors.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        /// <returns>List of authors.</returns>
        [HttpGet("GetAuthors")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAuthors([FromServices] IMediator mediator)
        {
            var req = new GetAuthorsQuery();
            var res = await mediator.Send(req);

            return res.ToActionResult();
        }
    }
}