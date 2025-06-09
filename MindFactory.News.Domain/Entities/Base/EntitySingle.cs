// <copyright file="EntitySingle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MindFactory.News.Domain.Entities.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class EntitySingle : EntitySingle<int> { }

    public abstract class EntitySingle<Tkey> : Entity
    {
        [Column(Order = 0)]
        public Tkey Id { get; set; }
    }
}