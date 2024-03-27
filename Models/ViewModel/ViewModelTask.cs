using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TodoList.Models.ViewModel;

public class ViewModelTask
{
    public uint Id { get; set; }

    [Required(ErrorMessage = "Es necesario elegir una categoria")]
    [Display(Name = "Categoria")]
    public uint CategoryId { get; set; }

    [Required(ErrorMessage = "Escriba su nombre.")]
    [MinLength(4, ErrorMessage = "Escriba al menos 5 caracteres.")]
    [MaxLength(50, ErrorMessage = "Escriba un máximo de 50 caracteres.")]
    [Display(Name = "Título")]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }
}
