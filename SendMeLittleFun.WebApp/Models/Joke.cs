using System.ComponentModel.DataAnnotations;

namespace SendMeLittleFun.WebApp.Models;

public class Joke {

    private Joke() { }
    public Joke(string _text) => JokeText = _text ?? throw new ArgumentNullException(nameof(_text));

    [Key]
    public int Id { get; set; }

    public string JokeText { get; set; }
}
