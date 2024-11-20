using Microsoft.AspNetCore.Mvc;
using Tareas_api.Models;

namespace Tareas_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TareaController : ControllerBase
    {
        //Se crea la lista
        private static List<Tareas> tareas = new List<Tareas>
        {
            new Tareas
            {
                id = 1,
                Titulo = "Estudiar",
                Descripcion = "Estudiar y repasar para el proximo certamen",
                Estado = "Pendiente"
            },
            new Tareas
            {
                id = 2,
                Titulo = "Comprar cereal",
                Descripcion = "Ya no hay cereal en la casa, toco comprar",
                Estado = "Pendiente"
            }
        };
        //Se muestra toda la lista
        [HttpGet]
        [Route("listar")]
        public dynamic listarTarea()
        {
            return tareas;
        }
        //Busca por tarea por su id
        [HttpGet]
        [Route("buscarPorId")]
        public dynamic listarTareaId(int _id)
        {
            var tarea = tareas.First(tareabuscar => tareabuscar.id == _id); //Busca el primero que coincide

            if (tarea != null)
            {
                return tarea;
            }
            else
            {
                return new 
                { 
                    success = false, 
                    message = "Tarea no encontrada"
                }; 
            }
        }
        //Guarda una nueva tarea
        [HttpPost]
        [Route("guardar")]
        public dynamic nuevaTarea(Tareas tarea)
        {
            tareas.Add(tarea); 

            return new
            {
                success = true,
                message = "Tarea registrada exitosamente",
                result = tarea
            };
        }
        //Elimina por su id
        [HttpDelete]
        [Route("eliminar")]
        public dynamic eliminarTarea(int _id)
        {
            var tarea = tareas.First(tareabuscar => tareabuscar.id == _id); //Busca el primero que coincide

            if (tarea != null)
            {
                tareas.Remove(tarea);
                return new
                {
                    success = true,
                    message = "Tarea eliminada exitosamente",
                    result = tarea //Muestra la tarea eliminada
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Tarea no encontrada"
                };
            }
        }
        //Actualiza una tarea
        [HttpPut]
        [Route("actualizar/{_id}")]
        public dynamic actualizarTarea([FromRoute] int _id, [FromBody] Tareas actualizar )
        {
            var tarea = tareas.First(tareabuscar => tareabuscar.id == _id); //Busca el primero que coincide

            if (tarea == null) 
            {
                return NotFound("Tarea no encontrada");
            }

            tarea.Titulo = actualizar.Titulo;
            tarea.Descripcion = actualizar.Descripcion;
            tarea.Estado = actualizar.Estado;

            return NoContent();
        }
    }
    
}
