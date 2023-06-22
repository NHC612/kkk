using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testthi.Models
{
    [Table("NhaCungCap")]
    public class NhaCungCap
    {
        [Key]
        public int? MaNhaCungCap {get; set; }
        public string? TenNhaCungCap {get; set; }
        
        

        
    }
}