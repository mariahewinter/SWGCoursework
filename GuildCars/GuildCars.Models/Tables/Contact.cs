using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Contact
    {
        public int ContactID { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter a message.")]
        public string Message { get; set; }
    }
}
