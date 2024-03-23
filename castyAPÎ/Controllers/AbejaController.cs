using castyAPÎ.Helpers;
using castyAPÎ.Models;
using castyAPÎ.Services;
using Microsoft.AspNetCore.Mvc;

namespace castyAPÎ.Controllers;

//Va con mapeo especifico. Api rest a un recurso. 
//Controllers clases especiales, vincualdas a HTTp que reciben el request, y los metodos que interactuan, se dice action o metodos. Estos action vana  devolver datos y status.
//HTTP devuelve Status Codes , rango de 2xx bien, 3xx redireccion, 4xx el usuario hizo algo mal en el request.
//Nuestra calse herede de otra clase es bueno. Es necesario. (ControllerBase)
//data notation []

[ApiController] //hace todo solito, sino las validaciones van a mano. 
[Route("api/[controller]")] //Que url va a tener. 5100, el nombre del controller.  

//Generar/Crear metodos para meter data, metodos de HTTP. GET POST PUT PATCH DELETE
//Se puede usar base de datos.

//Tengo que crear action para HTTP Get.- Para traer las abejas
// 2 get, uno para traerlos y otro para filtrarlos
//El primero: 

public class AbejaController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Abeja>> GetAbejas()
    {
        return Ok(AbejaDataStore.Current.Abejas); //Forma mas adecuada-

    }

    [HttpGet("{abejaId}")] //traer datos de api Get : 1 abeja sola-, desppues se pone parametro entre comillas ("{Parametros}")
    public ActionResult<Abeja> GetAbeja(int abejaId) // si no tuviera el [ApiController] tendria que poner asi : GetAbeja([FromRoute]int abejaId) no ahce falta porq el tipo de dato es simple
    {
        var abeja = AbejaDataStore.Current.Abejas.FirstOrDefault(x => x.Id == abejaId); //Variable, voy al data store y traigo current y traigo abejas y lo filtra traeme el primero o el default.

        if (abeja == null)
            return NotFound(Mensajes.Abeja.NotFound); //deveulvo 404 con este mensaje. y sino sigue de largo.

        return Ok(abeja);

    }

    //Aca vamos a hacer un Post. Es un action para crear otra abejas, pero solo le pido como dato el Nombre y el apellido. Para eso cree un model nuevo llamado AbejaInsert.
    [HttpPost]
    public ActionResult<Abeja> PostAbeja(AbejaInsert abejaInsert) //Viene del cuerpo del request, sino estuviera APIcontroller, vendria del FromBody.
    {
        //id de mandril nuevo.
        var maxAbejaId = AbejaDataStore.Current.Abejas.Max(x => x.Id);

        var abejaNueva = new Abeja()
        {
            Id = maxAbejaId + 1,
            Nombre = abejaInsert.Nombre,
            Apellido = abejaInsert.Apellido
        };


        AbejaDataStore.Current.Abejas.Add(abejaNueva);

        return CreatedAtAction(nameof(GetAbeja),
            new { abejaId = abejaNueva.Id },
            abejaNueva
        );

    }

    [HttpPut("{abejaId}")] //cual queremos modificar.
    public ActionResult<Abeja> PutAbeja([FromRoute]int abejaId, [FromBody]AbejaInsert abejaInsert) //recibe id de la abeja y los valores dentro
    {
        var abeja = AbejaDataStore.Current.Abejas.FirstOrDefault(x => x.Id == abejaId);
        if (abeja == null)
            return NotFound(Mensajes.Abeja.NotFound);
        abeja.Nombre = abejaInsert.Nombre;
        abeja.Apellido = abejaInsert.Apellido;

        return NoContent();
    }

    [HttpDelete("{abejaId}")]

    public ActionResult<Abeja> DeleteAbeja(int abejaId)
    {
        var abeja = AbejaDataStore.Current.Abejas.FirstOrDefault(x => x.Id == abejaId);
        if (abeja == null)
            return NotFound(Mensajes.Abeja.NotFound);
        
        AbejaDataStore.Current.Abejas.Remove(abeja);

        return NoContent();

    }



}
