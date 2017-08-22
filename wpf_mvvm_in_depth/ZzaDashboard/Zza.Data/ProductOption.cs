using System.ComponentModel.DataAnnotations;

namespace Zza.Data
{
    public class ProductOption
    {
        public int Factor { get; set; }

        [Key]
        public int Id { get; set; }

        public bool IsPizzaOption { get; set; }

        public bool IsSaladOption { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}