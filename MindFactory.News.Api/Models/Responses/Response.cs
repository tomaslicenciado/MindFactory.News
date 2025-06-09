// <copyright file="Response.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Api.Models.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class Response
    {
        public static Response<T> Fail<T>(string message, T data = default!)
        {
            return new Response<T>(data, message, true);
        }

        public static Response<T> Fail<T>(string message)
        {
            T data = default!;
            return new Response<T>(data, message, true);
        }

        public static Response<T> Ok<T>(T data, string message)
        {
            return new Response<T>(data, message, false);
        }

        public static Response<T> Ok<T>(string message)
        {
            T data = default!;
            return new Response<T>(data, message, false);
        }
    }
}