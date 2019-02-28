using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using WsSuperoProject.Services;
using WsSuperoProject.Models;

namespace WsSuperoProject.Controllers
{
    [RoutePrefix("api/Task")]
    public class TaskController : ApiController
    {
        [HttpPost]
        [Route("SalvarTask")]
        public HttpResponseMessage SalvarTask(string pTaskJson)
        {
            try
            {
                TaskModel TaskModel = new JavaScriptSerializer().Deserialize<TaskModel>(pTaskJson);

                var retornoJson = new JavaScriptSerializer().Serialize(new TaskService().SalvarTask(TaskModel));

                return Request.CreateResponse(HttpStatusCode.OK, retornoJson);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro na ação SalvarTask: " + e.Message);
            }
        }


        [HttpPost]
        [Route("DeletarTask")]
        public HttpResponseMessage DeletarTask(string pTaskJson)
        {
            try
            {
                TaskModel TaskModel = new JavaScriptSerializer().Deserialize<TaskModel>(pTaskJson);

                var retornoJson = new JavaScriptSerializer().Serialize(new TaskService().DeletarTask(TaskModel));

                return Request.CreateResponse(HttpStatusCode.OK, retornoJson);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro na ação DeletarTask: " + e.Message);
            }
        }

        [HttpGet]
        [Route("BuscarTask")]
        public HttpResponseMessage BuscarTask(string pTaskJson)
        {
            try
            {
                TaskModel TaskModel = new JavaScriptSerializer().Deserialize<TaskModel>(pTaskJson);

                var retornoJson = new JavaScriptSerializer().Serialize(new TaskService().BuscarTask(TaskModel));

                return Request.CreateResponse(HttpStatusCode.OK, retornoJson);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro na ação BuscarTask: " + e.Message);
            }
        }

        [HttpGet]
        [Route("CarregarTask")]
        public HttpResponseMessage CarregarTask()
        {
            try
            {
                var retornoJson = new JavaScriptSerializer().Serialize(new TaskService().CarregarTask());

                return Request.CreateResponse(HttpStatusCode.OK, retornoJson);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro na ação CarregarTask: " + e.Message);
            }
        }
    }
}