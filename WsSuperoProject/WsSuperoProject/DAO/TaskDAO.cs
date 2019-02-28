using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using WsSuperoProject.Models;

namespace WsSuperoProject.DAO
{
    public class TaskDAO : DbContext
    {
        public TaskDAO() : base("ApplicationServices")
        {

        }

        public DbSet<TaskModel> Task { get; set; }

        public TaskModel SalvarTask(TaskModel pTask)
        {
            var erroValidacao = pTask.ValidaTaskModel();

            if (!string.IsNullOrEmpty(erroValidacao))
                throw new Exception(erroValidacao);

            using (var dto = new TaskDAO())
            {
                dto.Task.AddOrUpdate(pTask);
                dto.SaveChanges();
            }

            return pTask;
        }

        public TaskModel DeletarTask(TaskModel pTask)
        {
            using (var dto = new TaskDAO())
            {
                dto.Task.Attach(pTask);
                dto.Task.Remove(pTask);
                dto.SaveChanges();
            }

            return pTask;
        }

        public List<TaskModel> CarregarTodosTask()
        {
            List<TaskModel> listaRetorno = new List<TaskModel>();
            using (var dto = new TaskDAO())
            {
                var query = from p in dto.Task
                            orderby p.Titulo
                            select p;

                foreach (var task in query)
                {
                    listaRetorno.Add(new TaskModel(task.Id, task.Titulo, task.Descricao, task.Status, task.DataCriacao, task.DataAlteracao));
                }
            }

            return listaRetorno;
        }

        public List<TaskModel> BuscarTask(TaskModel pTaskModel)
        {
            List<TaskModel> listaRetorno = new List<TaskModel>();
            using (var dto = new TaskDAO())
            {
                var query = from p in dto.Task
                            orderby p.Titulo
                            where 1 == 1
                            select p;

                query = FiltarObjeto(query, pTaskModel);

                foreach (var task in query)
                {
                    listaRetorno.Add(new TaskModel(task.Id, task.Titulo, task.Descricao, task.Status, task.DataCriacao, task.DataAlteracao));
                }
            }

            return listaRetorno;
        }

        private IQueryable<TaskModel> FiltarObjeto(IQueryable<TaskModel> pQuery, TaskModel pTaskModel)
        {
            if (pTaskModel.Id > 0)
                pQuery = pQuery.Where(c => c.Id == pTaskModel.Id);
            if (!string.IsNullOrEmpty(pTaskModel.Titulo))
                pQuery = pQuery.Where(c => c.Titulo.ToUpper().Contains(pTaskModel.Titulo.ToUpper()));
            if (!string.IsNullOrEmpty(pTaskModel.Descricao))
                pQuery = pQuery.Where(c => c.Descricao.ToUpper().Contains(pTaskModel.Descricao.ToUpper()));
            if (!string.IsNullOrEmpty(pTaskModel.Status))
                pQuery = pQuery.Where(c => c.Status.ToUpper().Contains(pTaskModel.Status.ToUpper()));

            return pQuery;
        }
    }
}