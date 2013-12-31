using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cpl_azure.Models
{

    public partial class webpages_UsersInRoles
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order=1)]
        public int UserId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order=2)]
        public int RoleId { get; set; }
    }
}