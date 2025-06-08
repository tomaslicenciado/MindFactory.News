using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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