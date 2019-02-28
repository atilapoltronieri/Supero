using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WsSuperoProject.Models
{
    public class TaskModel
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Descricao { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public DateTime DataCriacao { get; set; }
        [DataMember]
        public DateTime DataAlteracao { get; set; }

        public TaskModel()
        {

        }

        public TaskModel(int pId)
        {
            Id = pId;
        }

        public TaskModel(int pId, string pTitulo, string pDescricao, string pStatus, DateTime pDataCriacao, DateTime pDataAlteracao)
        {
            Id = pId;
            Titulo = pTitulo;
            Descricao = pDescricao;
            Status = pStatus;
            DataCriacao = pDataCriacao;
            DataAlteracao = pDataAlteracao;
        }

        public string ValidaTaskModel()
        {
            string retorno = string.Empty;

            AtualizaDataCriacaoAtualizacao();

            if (string.IsNullOrEmpty(Titulo))
                retorno += "A Task necessita ter um Título!";
            if (string.IsNullOrEmpty(Status))
                retorno += "A Task necessita ter um Status!";

            return retorno;
        }

        private void AtualizaDataCriacaoAtualizacao()
        {
            if (DataCriacao == DateTime.MinValue)
                DataCriacao = DateTime.Now;

            DataAlteracao = DateTime.Now;
        }
    }
}