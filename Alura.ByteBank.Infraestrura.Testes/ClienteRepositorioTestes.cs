using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrura.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _repositorio;
        public ClienteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IClienteRepositorio>();
        }



        [Fact]
        public void TestaObterTodosClientes()
        {
            //Arrage
            //var _repositorio = new ClienteRepositorio();

            //Act
            List<Cliente> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
            Assert.Equal(5, lista.Count);
        }

        [Fact]
         public void TestaObterClientePorId()
        {
            Cliente cliente = _repositorio.ObterPorId(1);

            Assert.NotNull(cliente);
            Assert.Equal(1, cliente.Id);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void TestaObterClientePorVariosIds(int id)
        {
            Cliente cliente = _repositorio.ObterPorId(id);

            Assert.NotNull(cliente);
            Assert.Equal(id, cliente.Id);

        }

        [Fact]
        public void TestaInsereUmNovoClienteNaBaseDeDados()
        {
            var novoCliente = new Cliente()
            {
                Nome = "Jack Bower",
                CPF = "571.207.720-58",
                Identificador = Guid.NewGuid(),
                Profissao = "Piloto",
                
            };

            var resultado = _repositorio.Adicionar(novoCliente);

            Assert.True(resultado);
        }

        [Fact]
        public void TestaAtualizarInformacaoDeterminadoCliente()
        {
            var cliente = _repositorio.ObterPorId(1);

            cliente.Nome = "Alexandre Carlos";
            cliente.Profissao = "Desenvolvedor";

            var resultado = _repositorio.Atualizar(1, cliente);

            Assert.True(resultado);
        }

        [Fact]
        public void TestaRemoverInformacaoDeterminadCliente()
        {
            var resultado = _repositorio.Excluir(9);

            Assert.True(resultado);
        }


        [Fact]
        public void TestaExcecaoConsultaPorClientePorId()
        {
            Assert.Throws<Exception>(() => _repositorio.ObterPorId(15)
            );
        }

    }



}
