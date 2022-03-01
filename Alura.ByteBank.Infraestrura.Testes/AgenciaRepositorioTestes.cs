using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrura.Testes.Servico;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.Infraestrura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repositorio;
        public ITestOutputHelper SaidaConsoleTeste { get; set; }
        public AgenciaRepositorioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor executado com sucesso!");

            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestarObterTodasAgencias()
        {

            List<Agencia> lista = _repositorio.ObterTodos();

            Assert.NotNull(lista);
            Assert.Equal(3, lista.Count);

        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            Agencia agencia = _repositorio.ObterPorId(1);

            Assert.NotNull(agencia);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(7)]
        public void TestaObterAgenciasPorVariosId(int id)
        {
            var agencia = _repositorio.ObterPorId(id);

            Assert.NotNull(agencia);
            Assert.Equal(id, agencia.Id);
        }

        [Fact]
        public void TestaInsereUmaNovaAgenciaNaBaseDeDados()
        {
            string nome = "Agência Guarapari";
            int numero = 125982;
            Guid identificador = Guid.NewGuid();
            string endereco = "Rua: 7 de Setembro - Centro";

            var agencia = new Agencia()
            {
                Nome = nome,
                Numero = numero,
                Identificador = identificador,
                Endereco = endereco

            };

            var retorno = _repositorio.Adicionar(agencia);

            Assert.True(retorno);
        }

        [Fact]
        public void TestaAtualizarInformacaoDeterminadaAgencia()
        {
            var agencia = _repositorio.ObterPorId(9);
            var nomeNovo = "Agencia Nova";
            agencia.Nome = nomeNovo;

            var atualizado = _repositorio.Atualizar(9, agencia);

            Assert.True(atualizado);
        }

        [Fact]
        public void TestaRemoverInformacaoDeterminadaAgencia()
        {
            var atualizado = _repositorio.Excluir(9);

            Assert.True(atualizado);
        }

        [Fact]
        public void TestaExcecaoConsultaPorAgenciaPorId()
        {
            Assert.Throws<Exception>(
                () => _repositorio.ObterPorId(33)
            );
        }

        [Fact]
        public void TestaAdionarAgenciaMock()
        {
            //Arrange
            var agencia = new Agencia()
            {
                Nome = "Agência Amaral",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();

            //Actcurs
            var adicionado = repositorioMock.AdicionarAgencia(agencia);

            //Assert
            Assert.True(adicionado);
        }

        [Fact]
        public void TestaObterAgenciaMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarAgencias();

            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarAgencias());
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Destrutor invocado.");
        }
    }
}
