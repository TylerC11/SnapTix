using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnapTix.Models
{
    public class Purchase
    {
        // Primary key
        public int PurchaseId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerTicket { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public string BuyerEmail { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;


        // Foreign Key
        public int SportId { get; set; }

        // Navigation property
        public Sport? Sport { get; set; }

    }
}
