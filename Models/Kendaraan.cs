using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.netCoreMvcCrud.Models
{
    public class Kendaraan
    {
        [Key]
        public int? id { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("name")]
        public string nama{ get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("model")]
        public string model { get; set; }

        [Column(TypeName = "varchar(100)")]
        [DisplayName("merek")]
        public string merek { get; set; }

		[Column(TypeName = "varchar(100)")]
		public string transmisi { get; set; }

		[Column(TypeName = "int")]
        public int tahun { get; set; }

        [Column(TypeName = "int")]
        public int harga { get; set; }
    }
}
