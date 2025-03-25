using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Momen1.DAL.Models
{
   public class AppUser :IdentityUser
    {
        public string  FirtName { get; set; }
        public string LastName { get; set; }
        public bool  IsAgree { get; set; }


    }
}
