using System.ComponentModel.DataAnnotations;

namespace castyAPÎ;

public class AbejaInsert
{
    [Required]
    [MaxLength(50)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;
}
