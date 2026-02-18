using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApp.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserId { get; set; }

        public int StockId { get; set; }

        public appUser AppUser { get; set; }

        public Stock Stock { get; set; }


    }
}
