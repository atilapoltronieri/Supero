using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WsSuperoProject.DAO;
using WsSuperoProject.Models;

namespace WsSuperoProject.Services
{
    public class TaskService
    {
        public TaskModel SalvarTask(TaskModel pTaskModel)
        {
            return new TaskDAO().SalvarTask(pTaskModel);
        }

        public TaskModel DeletarTask(TaskModel pTaskModel)
        {
            return new TaskDAO().DeletarTask(pTaskModel);
        }

        public List<TaskModel> BuscarTask(TaskModel pTaskModel)
        {
            return new TaskDAO().BuscarTask(pTaskModel);
        }

        public List<TaskModel> CarregarTask()
        {
            return new TaskDAO().CarregarTodosTask();
        }
    }
}