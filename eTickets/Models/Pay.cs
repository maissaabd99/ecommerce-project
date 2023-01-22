using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Pay
    {
        public int Id { get; set; }
        [Required, StringLength(20, MinimumLength = 10)]
        public string CardName{ get; set; }
        [Required, StringLength(15)]
        public long CardNumber { get; set; }
        [Required, StringLength(30)]
        public DateTime ExpDate { get; set; }
        [Required, StringLength(4)]
        public string SecurityCode { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }


    }
}
