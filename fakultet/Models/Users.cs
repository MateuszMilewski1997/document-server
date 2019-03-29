using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakultet.Models
{
    public class Users
    {

        [Key] [Column(TypeName = "int")] public int? Id { get; set; }
        [Required] [Column(TypeName = "varchar(20)")] public string Login { get; set; }
        [Required] [Column(TypeName = "varchar(20)")] public string Password { get; set; }
        [Required] [Column(TypeName = "varchar(40)")] public string Email { get; set; }
        [Required] [Column(TypeName = "int")] public int Role { get; set; }

        [Required] [ForeignKey("Role")] public Roles roles {get; set;}
    }
}
