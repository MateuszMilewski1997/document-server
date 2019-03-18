using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fakultet.Models
{
    public class Roles
    {



        [Key] [Column(TypeName  = "int")] public int Id { get; set; }
        [Required] [Column(TypeName = "varchar(20)")] public string Role_Name { get; set; }
        

    }
}
