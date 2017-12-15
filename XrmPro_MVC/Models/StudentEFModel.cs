using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XrmPro_MVC.Models
{
    public class StudentEFModel
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }


        [Required]
        [StringLength(20, ErrorMessage = "Max Length 20")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "only letters and digits")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max Length 50")]
        [RegularExpression(@"\w+@\w+\.\w{2,3}", ErrorMessage = "example@xrmpro.ru")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Max Length 50")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "only letters and digits")]
        [Required]
        public string Git { get; set; }
    }
}
