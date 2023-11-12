using System.ComponentModel.DataAnnotations;

namespace SendMeLittleFun.WebApp.Models;

public class User {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Надо ввести адрес почты...")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Надо ввести своё имя...")]
    [Display(Name = "Имя")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Надо выбрать график отправки...")]
    [Display(Name = "График отправки")]
    public string? Schedule { get; set; }

    public DateTime? RegDate { get; set; }
}
