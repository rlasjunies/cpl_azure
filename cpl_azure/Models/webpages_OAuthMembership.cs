using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cpl_azure.Models
{

    public partial class webpages_OAuthMembership
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }

    }
}