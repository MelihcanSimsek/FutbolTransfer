using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Club:IEntity
    {
        public int Id { get; set; }
        public string? Team { get; set; }
        public string? Image { get; set; }
    }
}
