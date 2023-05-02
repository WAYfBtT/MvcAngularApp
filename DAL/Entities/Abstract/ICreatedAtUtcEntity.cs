using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Abstract
{
    internal interface ICreatedAtUtcEntity
    {
        public DateTime CreatedAtUtc { get; set; }
    }
}
