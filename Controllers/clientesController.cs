using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_bd.Models;
namespace web_api_bd.Controllers{

    [Route("api/[Controller]")]
    public class clientesController : Controller{
       
        private conexion dbConexion;
        public clientesController(){
            dbConexion = conectar.Create();
        }

        [HttpGet]
        public ActionResult Get(){
            return Ok(dbConexion.clientes.ToArray());
        
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id){
            //var Clientes = dbConexion.clientes.SingleOrDefault(a => a.idclientes == id);
            var Clientes = await dbConexion.clientes.FindAsync(id);
            if(Clientes !=null){
                return Ok(Clientes);
            
            } else{
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] clientes Clientes){
            if(ModelState.IsValid){
                dbConexion.clientes.Add(Clientes);
                //dbConexion.SaveChanges();
                await dbConexion.SaveChangesAsync();
                return Ok(Clientes);
            
            }else{
                return NotFound();
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] clientes Clientes){
            var v_clientes = dbConexion.clientes.SingleOrDefault(a => a.idclientes == Clientes.idclientes);
            if(v_clientes != null && ModelState.IsValid){
                dbConexion.Entry(v_clientes).CurrentValues.SetValues(Clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }else{
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id){
            var Clientes = dbConexion.clientes.SingleOrDefault(a => a.idclientes == id);
            if (Clientes != null){
                dbConexion.clientes.Remove(Clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();

            } else{
                return NotFound();
            }
        }
    }
}