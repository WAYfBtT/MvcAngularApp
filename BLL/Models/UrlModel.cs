using BLL.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UrlModel<TUserModelKey> : BaseModel
    {
        [Required]
        [MaxLength(2048)]
        [RegularExpression(@"^(https?://)([^\s/:?#]+\.?)+([/:?#]|$)")]
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public TUserModelKey CreatedBy { get; set; }
    }

    public class UrlModel : UrlModel<int>
    {
    }
}
