// <copyright file="Response{T}.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Api.Models.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Initializes a new instance of the <see cref="Response{T}"/> class.
    /// </summary>
    /// <param name="data">Data.</param>
    /// <param name="msg">Msg.</param>
    /// <param name="error">Error.</param>
    public class Response<T>(T data, string msg, bool error)
    {
        public T Data { get; set; } = data;

        public string Message { get; set; } = msg;

        public bool IsError { get; set; } = error;
    }
}