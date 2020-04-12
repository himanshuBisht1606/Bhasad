using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class ProductTypeModel : BaseModel
    {
        public int ProductTypeId { get; set; }
        [Required(ErrorMessage ="Product Type is required")]
        [MaxLength(5,ErrorMessage ="Product Type length should be less than or equal to 5")]
        [MinLength(1,ErrorMessage ="Product Type length should be greater than 0")]
        public string ProductType { get; set; }
    }
}
