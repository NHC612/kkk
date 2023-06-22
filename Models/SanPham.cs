using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testthi.Models
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        public string? MaSanPham {get; set; }
        public string? TenSanPham {get; set; }
         public int? MaNhaCungCap {get; set; }
        [ForeignKey("MaNhaCungCap")]
        public NhaCungCap? NhaCungCap {get; set;}
        

        
    }
}