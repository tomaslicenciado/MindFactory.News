// <copyright file="IApplicationDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Application.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MindFactory.News.Domain.Entities;

    public interface IApplicationDbContext
    {
        public DbSet<NewsItem> NewsItems { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Editorial> Editorials { get; set; }

        /// <summary>
        /// Guarda cambios en base de datos.
        /// </summary>
        /// <param name="cancellationToken">Cancellationtoken.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}