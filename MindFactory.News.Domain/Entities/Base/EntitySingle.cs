using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MindFactory.News.Domain.Entities.Base
{
    public abstract class EntitySingle : EntitySingle<int> { }
    public abstract class EntitySingle<Tkey> : Entity
    {
        [Column(Order = 0)]
        public Tkey Id { get; set; }
    }
}