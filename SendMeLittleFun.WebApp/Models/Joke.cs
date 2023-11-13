using System.ComponentModel.DataAnnotations;

namespace SendMeLittleFun.WebApp.Models;

public class Joke {
    [Key]
    public int Id { get; set; }

    public string Text { get; set; }
}
