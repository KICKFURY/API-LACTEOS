//using API_LACTEOS.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Data;

//namespace API_LACTEOS.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RolesController : ControllerBase
//    {
//        public readonly LacteosLaGranja1Context _dbcontext;

//        public RolesController(LacteosLaGranja1Context _context)
//        {
//            _dbcontext = _context;
//        }

//        [HttpGet]
//        [Route("Lista")]
//        public IActionResult Lista()
//        {

//            List<Role> lista = new();

//            try
//            {
//                lista = _dbcontext.Roles.ToList();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
//            }

//        }

//        [HttpGet]
//        [Route("Obtener/{idRole:int}")]
//        public IActionResult Obtener(int idRole)
//        {
//            Role oRole = _dbcontext.Roles.Find(idRole);

//            if (oRole == null)
//            {
//                return BadRequest("Role no encontrado");
//            }

//            try
//            {
//                oRole = _dbcontext.Roles.Where(p => p.Id == idRole).FirstOrDefault();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oRole });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = oRole });
//            }
//        }

//        [HttpPost]
//        [Route("Guardar")]
//        public IActionResult Guardar([FromBody] Role objeto)
//        {
//            try
//            {
//                _dbcontext.Roles.Add(objeto);
//                _dbcontext.SaveChanges();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
//            }
//        }

//        [HttpPut]
//        [Route("Editar")]
//        public IActionResult Editar([FromBody] Role objeto)
//        {
//            Role oRole = _dbcontext.Roles.Find(objeto.Id);

//            if (oRole == null)
//            {
//                return BadRequest("Rol no encontrado");
//            }
//            try
//            {
//                oRole.Nombre = objeto.Nombre is null ? oRole.Nombre : objeto.Nombre;
               
//                _dbcontext.Roles.Update(oRole);
//                _dbcontext.SaveChanges();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });


//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
//            }



//        }

//        [HttpDelete]
//        [Route("Eliminar/{id:int}")]
//        public IActionResult Eliminar(int Id)
//        {
//            Role oRole = _dbcontext.Roles.Find(Id);

//            if (oRole == null)
//            {
//                return BadRequest("Role no encontrado");
//            }

//            try
//            {
//                _dbcontext.Roles.Remove(oRole);
//                _dbcontext.SaveChanges();

//                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
//            }
//        }
//    }
//}
