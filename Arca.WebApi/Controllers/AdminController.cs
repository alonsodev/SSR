using Business.Logic;
using Domain.Entities;
using Domain.Entities.Movil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arca.WebApi.Controllers
{
    /// <summary>
    /// admin controller class for testing security token with role admin
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        [HttpPost]
        [Route("concept")]
        public IHttpActionResult Concept(ConceptFilterLiteViewModel filter)
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();

            ConceptDetailLiteViewModel obj = oConceptBL.ObtenerLite(filter);



            var adminFake = new
            {
                // this is what datatables wants sending back

                data = obj
            };
            return Ok(adminFake);
        }
        [HttpPost]
        [Route("concepts")]
        public IHttpActionResult Concepts(ConceptsFilterLiteViewModel filter)
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
          
            List<ConceptLiteViewModel> lista = oConceptBL.ObtenerPorCalificarMovil(filter);

            

            var adminFake = new
            {
                // this is what datatables wants sending back
              
                data = lista
            };
            return Ok(adminFake);
        }

        [HttpPost]
        [Route("draftlaw")]
        public IHttpActionResult DraftLaw(DraftLawFilterLiteViewModel filter)
        {
            DraftLawBL oBL = new DraftLawBL();
           
            List<DraftLawLiteViewModel> lista = oBL.ObtenerProyectosLeyConConceptosPorCalificar(filter);



            var result = new
            {
                // this is what datatables wants sending back
               
                data = lista
            };
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var adminList = new string[] { "admin-1", "admin-2", "admin-3" };
            return Ok(adminList);
        }
    }
}
