using castyAPÎ.Helpers;
using castyAPÎ.Models;
using castyAPÎ.Services;
using Microsoft.AspNetCore.Mvc;
namespace castyAPÎ.Controllers;


//BoilerPlate codigo repetitivo no tiene tanto contenido, public class etc.

[ApiController]
[Route("api/abeja/{abejaId}/[controller]")] //pasar por abeja, busco id y despues controller

public class HabilidadController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Habilidad>> GetHabilidades(int abejaId)
    {
        var abeja = AbejaDataStore.Current.Abejas.FirstOrDefault(x => x.Id == abejaId);

        if (abeja == null)
            return NotFound(Mensajes.Abeja.NotFound);
        return Ok(abeja.Habilidades);
    }

    [HttpGet("{habilidadId}")]
    public ActionResult<Habilidad> GetHabilidad(int abejaId, int habilidadId)
    {
        var abeja = AbejaDataStore.Current.Abejas.FirstOrDefault(x => x.Id == abejaId);

        if (abeja == null)
            return NotFound(Mensajes.Abeja.NotFound);

        var habilidad = abeja.Habilidades?.FirstOrDefault(h => h.Id == habilidadId); //signo de pregunta los que sigue hacelo solamente si el objeto no es nulo sino va a dar exception.
        if(habilidad == null)
            return NotFound(Mensajes.Habilidad.NotFound);

        return Ok(habilidad);
    }



    [HttpPost]
    public ActionResult<Habilidad> PostHabilidad(int abejaId, HabilidadInsert habilidadInsert)
    {

        var abeja = AbejaDataStore.Current.Abejas.FirstOrDefault(x => x.Id == abejaId);

        if (abeja == null)
            return NotFound(Mensajes.Abeja.NotFound);
        
        var habilidadExistente = abeja.Habilidades.FirstOrDefault(h => h.Nombre == habilidadInsert.Nombre);

        if (habilidadExistente != null)
            return BadRequest(Mensajes.Habilidad.NombreExistente);

        var maxHabilidad = abeja.Habilidades.Max(h => h.Id);

        var habilidadNueva = new Habilidad() {
            Id = maxHabilidad + 1,
            Nombre = habilidadInsert.Nombre,
            Potencia = habilidadInsert.Potencia
        };

        abeja.Habilidades.Add(habilidadNueva);

        return CreatedAtAction(nameof(GetHabilidad),
            new { abejaId = abejaId, habilidadId = habilidadNueva.Id },
            habilidadNueva
        );

    }

    
    [HttpPut("{habilidadId}")]
    public ActionResult<Habilidad> PutHabilidad(int abejaId, int habilidadId, HabilidadInsert habilidadInsert)
    {   
        //Validaciones
        var abeja = AbejaDataStore.Current.Abejas.FirstOrDefault(x => x.Id == abejaId);

        if (abeja == null)
            return NotFound(Mensajes.Abeja.NotFound);
        
        var habilidadExistente = abeja.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);

        if(habilidadExistente == null)
            return NotFound(Mensajes.Habilidad.NotFound);
        
        var habilidadMismoNombre = abeja.Habilidades?
            .FirstOrDefault(h => h.Id != habilidadId && h.Nombre == habilidadInsert.Nombre);
        
        if (habilidadMismoNombre != null)
            return BadRequest(Mensajes.Habilidad.NombreExistente);
        
        //Asignacion
        habilidadExistente.Nombre = habilidadInsert.Nombre;
        habilidadExistente.Potencia = habilidadInsert.Potencia;

        return NoContent();
    }

    [HttpDelete("{habilidadId}")]
    public ActionResult<Habilidad> DeleteHabilidad(int abejaId, int habilidadId)
    {   
        //Validaciones
        var abeja = AbejaDataStore.Current.Abejas.FirstOrDefault(x => x.Id == abejaId);

        if (abeja == null)
            return NotFound(Mensajes.Abeja.NotFound);
        
        var habilidadExistente = abeja.Habilidades?.FirstOrDefault(h => h.Id ==  habilidadId);

        if (habilidadExistente == null)
            return NotFound(Mensajes.Habilidad.NotFound);
        
        //Eliminacion
        abeja.Habilidades?.Remove(habilidadExistente);

        return NoContent();
    }

}
