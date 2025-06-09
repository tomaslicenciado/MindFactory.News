// <copyright file="EntityAuditable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Domain.Entities.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class EntityAuditable : EntityAuditable<int> {}

    public abstract class EntityAuditable<Tkey> : EntitySingle<Tkey>
    {
        public EntityAuditable()
        {
            CreatedDateTime = DateTime.UtcNow;
        }

        public int CreatedUserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUserId { get; set; }
        
        public DateTime? UpdatedDateTime { get; set; }

        public bool Enabled { get; set; } = true;
    }
}