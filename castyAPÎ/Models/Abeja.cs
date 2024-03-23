//Quiero que este dentro del namespace .models
namespace castyAPÎ.Models; 


//clase publica, porque se puede utilizar desde cualquier lugar.
//codeo prop para tener la property. Creo el id (identificador)
public class Abeja
{
    public int Id { get; set; } //id unico para identificar la abeja en mi sistema.

    public string Nombre { get; set; } = string.Empty; //Empty se incializa para que no quede null.

    public string Apellido { get; set; } = string.Empty; //Tmbien incializa.

    public List<Habilidad>? Habilidades { get; set;} = new List<Habilidad>(); //Lista de hablidades se va a llamar Habilidades ? es para que sea null

}
