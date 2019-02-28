var app = angular.module('myApp', ['ui.mask','ngAnimate', 'toaster', 'angularSpinner', 'bsTable']);

app.service('spinnerService', function () {
	this.show = function () {
		document.getElementById('mySpinner').className = "spinner-design"
	},

	this.hide = function () {
		document.getElementById('mySpinner').className = "spinner-design ng-hide"
	}
})

app.controller('taskCtrl', ['$scope', '$http', '$window', '$location', 'toaster', 'spinnerService', function($scope, $http, $window, $location, toaster, spinnerService) {
	
	this.listaTasks = [];
	this.listaStatus = [];
	this.localHost = "";
	
	$scope.carregarTasks = function () {
			
		var source = this;	
		source.localHost = "http://localhost:26198/api/";
		source.listaTasks = [];
		source.listaTasksFiltro = [];
		source.listaTasksShow = [];
		source.filtro = {};
		source.objetoTela = {};
		source.paginacao = {};
		source.paginacao.paginaAtual = 1;
		source.carregarStatus();
		
		//spinnerService.show();
		
		$http.get(source.localHost + "Task/CarregarTask")
			.success (function(response){
			Materialize.toast('Tasks Carregadas', 4000, 'green');
			source.listaTasks = JSON.parse(response);
			
			for(i = 0; i < source.listaTasks.length; i++)	
			{				 
				source.listaTasks[i].DataCriacao = new Date(parseInt(source.listaTasks[i].DataCriacao.slice(6, source.listaTasks[i].DataCriacao.length -2))).toLocaleDateString();
				source.listaTasks[i].DataAlteracao = new Date(parseInt(source.listaTasks[i].DataAlteracao.slice(6, source.listaTasks[i].DataAlteracao.length -2))).toLocaleDateString();				
			}
			
			source.aplicarFiltro();
		 })
		.error(function(response){			
			 Materialize.toast(response, 4000, 'red');
		 })
		.finally (function () {
		});
	}
	
	$scope.aplicarFiltro = function() {
		var source = this;		
		source.paginacao.paginaAtual = 1;
		source.listaTasksFiltro = source.listaTasks;
		
		if (source.filtro.Id != undefined && source.filtro.Id != "")
			source.listaTasksFiltro = Enumerable.from(source.listaTasksFiltro).where(function (x) {return x.Id == source.filtro.Id}).toArray();
		if (source.filtro.Titulo != undefined && source.filtro.Titulo != "")
			source.listaTasksFiltro = Enumerable.from(source.listaTasksFiltro).where(function (x) {return x.Titulo.indexOf(source.filtro.Titulo) >= 0}).toArray();
		if (source.filtro.Status != undefined && source.filtro.Status != "")
			source.listaTasksFiltro = Enumerable.from(source.listaTasksFiltro).where(function (x) {return x.Status.indexOf(source.filtro.Status) >= 0}).toArray();
		
		source.aplicarFiltroPaginacao();
	}
	
	$scope.aplicarFiltroPaginacao = function () {
		var source = this;
		
		source.listaTasksShow = source.listaTasksFiltro;
		//spinnerService.show();
		
		source.paginacao.registroInicial = ((source.paginacao.paginaAtual - 1) * 10) + 1;
		if (source.paginacao.paginaAtual * 10 < source.listaTasksShow.length)
			source.paginacao.registroFinal = (source.paginacao.paginaAtual * 10);
		else
			source.paginacao.registroFinal = source.listaTasksShow.length;
		
		source.paginacao.registrosTotais = source.listaTasksShow.length;
		
		source.listaTasksShow = source.listaTasksShow.slice(source.paginacao.registroInicial - 1, source.paginacao.registroFinal)
		
		source.listaTasksShow = Enumerable.from(source.listaTasksShow).orderBy(function (x) { return x.Titulo}).toArray();
		
		//spinnerService.hide();
	}
	
	$scope.validaProximaPagina = function () {
		
		var source = this;
		
		if (source.paginacao.paginaAtual * 10 < source.listaTasksFiltro.length)
			return false;
		
		return true;
	}
	
	$scope.validaAnteriorPagina = function () {
		
		var source = this;
		
		if (source.paginacao.paginaAtual > 1)
			return false;
		
		return true;
	}
	
	$scope.proximaPagina = function () {
		
		var source = this;
		//spinnerService.show();
		source.listaTasksShow = [];
		source.paginacao.paginaAtual = source.paginacao.paginaAtual + 1;
		source.aplicarFiltroPaginacao();
		
		//spinnerService.hide();
	}
	
	$scope.anteriorPagina = function () {
		
		var source = this;
		//spinnerService.show();
		source.listaTasksShow = [];
		source.paginacao.paginaAtual = source.paginacao.paginaAtual - 1;
		source.aplicarFiltroPaginacao();
		
		//spinnerService.hide();
	}
	
	$scope.validaSalvar = function () {
		
		var source = this;
		
		if (source.objetoTela.Titulo != undefined &&
			source.objetoTela.Status != undefined)		
			return false;
		
		return true;
	}
	
	$scope.novoTask = function () {
		
		var source = this;	
		source.objetoTela = {};
	}
	
	$scope.salvarTask = function () {
		
		var source = this;	
		
		var task = {};
		
		task.Id = source.objetoTela.Id;
		task.Titulo = source.objetoTela.Titulo;
		task.Status = source.objetoTela.Status;
		task.Descricao = source.objetoTela.Descricao;
		if (task.Id > 0) {
			task.DataCriacao = source.objetoTela.DataCriacao.slice(3, 5) + '/' + source.objetoTela.DataCriacao.slice(0, 2) + '/' + source.objetoTela.DataCriacao.slice(6, 10);
			task.DataAlteracao = source.objetoTela.DataAlteracao.slice(3, 5) + '/' + source.objetoTela.DataAlteracao.slice(0, 2) + '/' + source.objetoTela.DataAlteracao.slice(6, 10);
		}
		var pTaskJson = JSON.stringify(task);
		
		//spinnerService.show();

		$http.post(source.localHost + "Task/SalvarTask?pTaskJson=" + pTaskJson)
		.success (function(response){
			
			var retorno = JSON.parse(response);
			
			if (task.Id != undefined && task.Id != 0)
			source.listaTasks = Enumerable.from(source.listaTasks).where(function (x) { return x.Id != task.Id}).toArray();
					
			retorno.DataCriacao = new Date(parseInt(retorno.DataCriacao.slice(6, retorno.DataCriacao.length -2))).toLocaleDateString();
			retorno.DataAlteracao = new Date(parseInt(retorno.DataAlteracao.slice(6, retorno.DataAlteracao.length -2))).toLocaleDateString();
			source.listaTasks.push(retorno);
			source.listaTasks = Enumerable.from(source.listaTasks).orderBy(function (x) { return x.Id }).toArray();
			Materialize.toast("Registro Incluído com Sucesso.", 4000, 'green');
						
			source.aplicarFiltro();
		 })
		.error(function(response){
			 Materialize.toast(response, 4000, 'red');
			 //toaster.pop('error', "Login", response);
		 })
		.finally (function () {
			//spinnerService.hide();
		});
	}
	
	$scope.confirmarDeletarTask = function () {
		
		var source = this;	
		
		var task = {};
		
		task.Id = source.objetoTela.IdDeletar;
		var pTaskJson = JSON.stringify(task);
		
		//spinnerService.show();

		$http.post(source.localHost + "Task/DeletarTask?pTaskJson=" + pTaskJson)
		.success (function(response){
			
			source.listaTasks = Enumerable.from(source.listaTasks).where(function (x) { return x.Id != task.Id}).toArray();
						
			Materialize.toast("Registro Deletado com Sucesso.", 4000, 'green');
			
			source.aplicarFiltro();
		 })
		.error(function(response){
			 Materialize.toast(response, 4000, 'red');
		 })
		.finally (function () {
			//spinnerService.hide();
		});
	}
	
	$scope.deletarTask = function (IdDeletar) {
		
		var source = this;
		
		source.objetoTela.IdDeletar = IdDeletar;
	}
	
	$scope.editarTask = function(task) {
		var source = this;
		
		source.objetoTela.IdTask = "ID: " + task.Id;
		source.objetoTela.Id = task.Id;
		source.objetoTela.Titulo = task.Titulo;
		source.objetoTela.Descricao = task.Descricao;
		source.objetoTela.Status = task.Status;
		source.objetoTela.DataCriacao = task.DataCriacao;
		source.objetoTela.DataAlteracao = task.DataAlteracao;
	}
	
	$scope.carregarStatus = function(){
		var source = this;
		
		source.listaStatus = ['Novo', 'Em Atendimento', 'Concluído'];
	}
	
	$scope.carregarTasks();
}])
