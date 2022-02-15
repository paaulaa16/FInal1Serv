using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Influencers.Models;

namespace Influencers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfluController : ControllerBase
    {
        private readonly ModeloContext db;

        public InfluController(ModeloContext context)
        {
            db = context;
        }

        [HttpGet("1.1")]
        public async Task<ActionResult> Query1()
        {
            var list= await db.Contrato.Join(db.Producto, c=> c.ProductoId, o=> o.ProductoId, (c, o) => new {
                Influencers = c.InfluencerId,
                valorTotal = c.Cantidad * o.ValorEconomico
            }).OrderBy(b=>b.Influencers).ToListAsync();

            return Ok(new
            {
                ValorActual = "1.1",
                Descripcion = "total valor x Influencer (de todos)",
                Valores = list,
            });

        }

        [HttpGet("1.2")]
        public async Task<ActionResult> Query2(int influencer)
        {
             var list= await db.Contrato.Join(db.Producto, c=> c.ProductoId, o=> o.ProductoId, (c, o) => new {
                Influencers = influencer,
                valorTotal = c.Cantidad * o.ValorEconomico
            }).ToListAsync();

            return Ok(new
            {
                ValorActual = "1.2",
                Descripcion = "total valor x Influencer (de uno en concreto)",
                Valores = list,
            });

        }

        [HttpGet("2.1")]
        public async Task<ActionResult> Query3()
        {
            var list= await db.Contrato.Join(db.Influencer, c=> c.InfluencerId, o=> o.InfluencerId, (c, o) => new {
                Influencers = o.InfluencerId,
                Manager = o.Manager
            }).GroupBy(c => c.Manager).Select(f => new{
                Manager=f.Key,
                Influencers = f.Count(),
                TotalValor = db.Producto.Join(db.Contrato, e=> e.ProductoId, o=> o.ProductoId, (e , o) => new {
                totalValor = o.Cantidad * e.ValorEconomico
                })
            }).OrderBy(c=>c.Manager).ToListAsync();

            return Ok(new
            {
                ValorActual = "2.1",
                Descripcion = "total valor x Manager y cuantos influencers tiene (de todos)",
                Valores = list,
            });

        }

        [HttpGet("2.2")]
        public async Task<ActionResult> Query4(string manager)
        {
             var list= await db.Contrato.Join(db.Influencer, c=> c.InfluencerId, o=> o.InfluencerId, (c, o) => new {
                Influencers = o.InfluencerId,
                Manager = manager,
            }).GroupBy(c => c.Manager).Select(f => new{
                Manager=f.Key,
                Influencers = f.Count(),
                TotalValor = db.Producto.Join(db.Contrato, e=> e.ProductoId, o=> o.ProductoId, (e , o) => new {
                totalValor = o.Cantidad * e.ValorEconomico
                })
            }).OrderBy(b=>b.Manager).ToListAsync();

            return Ok(new
            {
                ValorActual = "2.2",
                Descripcion = "total valor x Manager y cuantos influencers tiene (de uno en concreto)",
                Valores = list,
            });
        }
        
        [HttpPost("Influencers")]
        public async Task<ActionResult> PostInfluencers(Models.Influencer influencers)
        {
            db.Influencer.Add(influencers);
            await db.SaveChangesAsync();
            return Ok(influencers);
        }

        [HttpPost("Productos")]
        public async Task<ActionResult<Models.Producto>> PostProductos(Models.Producto productos)
        {
            db.Producto.Add(productos);
            await db.SaveChangesAsync();
            return Ok(productos);
        }

        [HttpPost("Contratos")]
        public async Task<ActionResult<Models.Influencer>> PostContratos(Models.Contrato contratos)
        {
            db.Contrato.Add(contratos);
            await db.SaveChangesAsync();
            return Ok(contratos);
        }
    }
}
