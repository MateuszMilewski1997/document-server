using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakultet.Models
{
    public class DocumentStatusCOM
    {

         [Key] [Column(TypeName = "int")] public int? Id { get; set; }
         [Column(TypeName = "varchar(60)")] public string Name_Doc { get; set; }
         [Column(TypeName = "varchar(60)")] public string User_Mail { get; set; }
         [Column(TypeName = "varchar(60)")] public string Type_document { get; set; }
         [Column(TypeName = "varchar(60)")] public string Function_author { get; set; }
         [Column(TypeName = "varchar(60)")] public string Send_Date { get; set; }
         //[Column(TypeName = "DateTime")] public DateTime Send_Date { get; set; }
         [Column(TypeName = "varchar(500)")] public string Document_Description { get; set; }
         [Column(TypeName = "varchar(60)")] public string Status { get; set; }
         [Column(TypeName = "nvarchar(MAX)")] public string Comment { get; set; }

        //  add table with comments to documents 

    }
}
