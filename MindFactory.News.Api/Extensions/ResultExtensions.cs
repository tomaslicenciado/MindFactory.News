// <copyright file="ResultExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Api.Extensions
{
    using CSharpFunctionalExtensions;
    using Microsoft.AspNetCore.Mvc;

    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Maybe<T> result)
        {
            return result.Match(Some: GenerateOkResult, None: GenerateNotFoundResult);
        }

        public static IActionResult ToOkOrNotFoundActionResult<T>(this Result<T> result)
        {
            return result.Match(onSuccess: GenerateOkResult, onFailure: GenerateNotFoundResult);
        }

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            return result.Match(onSuccess: GenerateOkResult, onFailure: GenerateBadResult);
        }

        public static IActionResult ToActionOkResult<T>(this Result<T> result, string msg)
        {
            return GenerateOkResult(result.Value, msg);
        }

        private static IActionResult GenerateNotFoundResult()
            => GenerateNotFoundResult("Element Not found.");

        private static IActionResult GenerateNotFoundResult(string msg)
            => new NotFoundObjectResult(Models.Responses.Response.Fail(msg, string.Empty));

        private static OkObjectResult GenerateOkResult<T>(T value)
        {
            return new OkObjectResult(Models.Responses.Response.Ok(value, "OK"));
        }

        private static OkObjectResult GenerateOkResult<T>(T value, string msg)
        {
            return new OkObjectResult(Models.Responses.Response.Ok(value, msg));
        }

        private static IActionResult GenerateBadResult(string message)
            => new BadRequestObjectResult(Models.Responses.Response.Fail(message, string.Empty));
    }
}