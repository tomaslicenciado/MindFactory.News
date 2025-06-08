using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindFactory.News.Domain.Entities;

namespace MindFactory.News.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Editorial> Editorials { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}