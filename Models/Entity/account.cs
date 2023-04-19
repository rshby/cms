using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms.Models.Entity
{
    [Table("accounts")]
    public class account
    {
        // constructor
        public account()
        {
            CreatedAt = DateTime.Now.ToLocalTime(); // set default value DateTime.Now untuk property CreatedAt
        }

        [JsonProperty("id")]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [JsonProperty("email")]
        [Column("email")]
        public string? Email { get; set; }

        [Required]
        [Column("username")]
        [JsonProperty("username")]
        public string? Username { get; set; }

        [Required]
        [Column("password")]
        [JsonProperty("password")]
        public string? Password { get; set; }


        [Column("otp")]
        [JsonProperty("otp")]
        public string? Otp { get; set; }

        [Column("expired_otp")]
        [JsonProperty("expired_otp")]
        public DateTime? ExpiredOtp { get; set; }

        [Required]
        [Column("created_at")]
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Required]
        [Column("user_id")]
        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }
}
