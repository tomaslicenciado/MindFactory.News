// <copyright file="GetAuthorsResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace MindFactory.News.Application.Authors.Queries.GetAuthors
{
    public class GetAuthorsResponse
    {
        public List<AuthorData> Authors { get; set; }
    }

    public class AuthorData
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}