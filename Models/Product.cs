using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace AuctionProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsPublic { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? Image { get; set; }
        public float? PriceStart { get; set; }
        public float? PriceEnd { get; set; }
        public float? PriceJump { get; set; }
        public float? PriceFinish { get; set; }
        public ApplicationUser? User { get; set; }
        public ApplicationUser? UserGet { get; set; }
        public ApplicationUser? TestJoin { get; set; }
    }
}
