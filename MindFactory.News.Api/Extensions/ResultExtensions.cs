using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace MindFactory.News.Api.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Maybe<T> result)
            => result.Match(Some: GenerateOkResult, None: GenerateNotFoundResult);

        public static IActionResult ToOkOrNotFoundActionResult<T>(this Result<T> result)
            => result.Match(onSuccess: GenerateOkResult, onFailure: GenerateNotFoundResult);

        public static IActionResult ToActionResult<T>(this Result<T> result)
            => result.Match(onSuccess: GenerateOkResult, onFailure: GenerateBadResult);
        
        public static IActionResult ToActionOkResult<T>(this Result<T> result, string msg)
            => GenerateOkResult(result.Value, msg);

        private static IActionResult GenerateNotFoundResult()
            => GenerateNotFoundResult("Element Not found.");
        private static IActionResult GenerateNotFoundResult(string msg)
            => new NotFoundObjectResult(Models.Responses.Response.Fail(msg, ""));

        private static IActionResult GenerateOkResult<T>(T value)
            => new OkObjectResult(Models.Responses.Response.Ok(value, "OK"));
        
        private static IActionResult GenerateOkResult<T>(T value, string msg)
            => new OkObjectResult(Models.Responses.Response.Ok(value, msg));

        private static IActionResult GenerateBadResult(string message)
            => new BadRequestObjectResult(Models.Responses.Response.Fail(message, ""));
    }
}