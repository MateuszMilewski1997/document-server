using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fakultet.Models
{
    public class Documents
    {

        [Key] [Column(TypeName = "int")] public int? Id { get; set; }
        [Required] [Column(TypeName = "varchar(60)")] public string Name_Doc { get; set; }
        [Required] [Column(TypeName = "varchar(60)")] public string User_Mail { get; set; }
        [Required] [Column(TypeName = "varchar(60)")] public string Type_document { get; set; }
        [Required] [Column(TypeName = "varchar(60)")] public string Function_author { get; set; }
        [Required] [Column(TypeName = "varchar(60)")] public string Send_Date { get; set; }
        [Required] [Column(TypeName = "varchar(500)")] public string Document_Description { get; set; }
        [Required] [Column(TypeName = "varchar(60)")] public string Status { get; set; }

        //  add table with comments to documents 

    }
}
