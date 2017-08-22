using System.ComponentModel.DataAnnotations;

namespace Zza.Data
{
    public class Product
    {
        public string Description { get; set; }

        public bool HasOptions { get; set; }

        [Key]
        public int Id { get; set; }

        public string Image { get; set; }

        public bool IsVegetarian { get; set; }

        public string Name { get; set; }

        public string SizeIds { get; set; }

        public string Type { get; set; }

        public bool WithTomatoSauce { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}