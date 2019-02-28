using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WsSuperoProject.Models;

namespace WsSuperoProject.Tests.Models
{
    [TestClass]
    public class TaskModelTest
    {
        [TestMethod]
        public void TaskModelSemTitulo()
        {
            TaskModel taskModel = new TaskModel(1, string.Empty, "Teste Erro sem Título", "Novo", DateTime.Now, DateTime.Now);
            var retorno = taskModel.ValidaTaskModel();
            Assert.AreEqual("A Task necessita ter um Título!", retorno);
        }

        [TestMethod]
        public void TaskModelSemDescricao()
        {
            TaskModel taskModel = new TaskModel(1, "Teste Erro sem Status", string.Empty, string.Empty, DateTime.Now, DateTime.Now);
            var retorno = taskModel.ValidaTaskModel();
            Assert.AreEqual("A Task necessita ter um Status!", retorno);
        }

        [TestMethod]
        public void TaskModelComTituloDescricao()
        {
            TaskModel taskModel = new TaskModel(1, "Teste sem Erro", "Teste sem Erro", "Novo", DateTime.Now, DateTime.Now);
            var retorno = taskModel.ValidaTaskModel();
            Assert.AreEqual(string.Empty, retorno);
        }
    }
}
