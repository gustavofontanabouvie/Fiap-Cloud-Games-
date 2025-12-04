using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain
{
    public class UserLibrary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime AdquiredAt { get; set; }

        public decimal PricePaid { get; set; }

        public Guid UserId { get; set; }
        public Guid GameId { get; set; }

        public string GameTitle { get; set; }

        public string CoverImageUrl { get; set; }

        public virtual User User { get; set; }

    }
}
