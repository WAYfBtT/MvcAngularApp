using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Abstract
{

    public class BaseModel<TKey> where TKey : struct
    {
        public TKey Id { get; set; } = default;
    }

    public class BaseModel : BaseModel<int>
    {
    }
}
