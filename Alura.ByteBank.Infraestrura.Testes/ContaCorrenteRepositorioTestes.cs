using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes;
using Alura.ByteBank.Infraestrutura.Testes.DTO;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.Infraestrura.Testes
{
    public class ContaCorrenteRepositorioTestes
    {
        private readonly IContaCorrenteRepositorio _repositorio;
        public ContaCorrente _conta;
        public Cliente _cliente;
        public Agencia _agencia;

        public ContaCorrenteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();
            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IContaCorrenteRepositorio>();

            _conta = new ContaCorrente();
            _cliente = new Cliente();
            _agencia = new Agencia();
        }

        [Fact]
        public void TestaObterTodasContasCorrentes()
        {
            List<ContaCorrente> lista = _repositorio.ObterTodos();

            Assert.NotNull(lista);
        }

        [Fact]
        public void TestaObterContaCorrentePorId()
        {
            var conta = _repositorio.ObterPorId(1);


            Assert.NotNull(conta);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(7)]
        public void TestarObterVariasContasCorrentesPorId(int id)
        {
            var conta = _repositorio.ObterPorId(id);

            Assert.NotNull(conta);

            Assert.Equal(id, conta.Id);
        }

        [Fact]
        public void TestaAtualizaSaldoDeterminadaConta()
        {
            var conta = _repositorio.ObterPorId(1);
            double saldoNovo = 15;
            conta.Saldo = saldoNovo;

            var atualizado = _repositorio.Atualizar(1, conta);

            Assert.True(atualizado);
        }

        [Fact]
        public void TestaInserieUmaNovaContaCorrenteNoBancoDeDados()
        {
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Numero = 1258,
                Cliente = new Cliente()
                {
                    Nome = "Kent Nelson",
                    CPF = "947.798.020-08",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Id = 1,
                    Endereco = "Rua das Flores, 25",
                    Numero = 147
                }
            };


            /* _conta.Saldo = 10;
             _conta.Identificador = Guid.NewGuid();

             _cliente.Nome = "Kent Nelson";
             _cliente.CPF = "486.074.980-45";
             _cliente.Identificador = Guid.NewGuid();
             _cliente.Profissao = "Bancário";
             _cliente.Id = 9;

              _conta.Cliente = _cliente;

             _agencia.Nome = "Agencia Central Coast City";
             _agencia.Identificador = Guid.NewGuid();
             _agencia.Id = 8;
             _agencia.Endereco = "Rua das Flores, 25";
             _agencia.Numero = 147;

               _conta.Agencia = _agencia;*/


            var retorno = _repositorio.Adicionar(conta);

            Assert.True(retorno);


        }

        [Fact]
        public void TestaAtualizarInformacoesDeUmaDeterminadaConta()
        {
            var conta = _repositorio.ObterPorId(10);

            conta.Numero = 1589;

            var atualizado = _repositorio.Atualizar(10, conta);

            Assert.True(atualizado);
        }

        [Fact]
        public void TestaRemoverInformacaoDeterminadaConta()
        {
            var atualizado = _repositorio.Excluir(9);

            Assert.True(atualizado);
        }

        [Fact]
        public void TestaExcecaoConsultaPorContaPorId()
        {
            Assert.Throws<Exception>(() => _repositorio.ObterPorId(55));
        }

        [Fact]
        public void TestaConsultaPix()
        {
            //Arrage
            var guid = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a");
            var pix = new PixDTO() { Chave = guid, Saldo = 10 };

            var pixRepositorioMock = new Mock<IPixRepositorio>();
            pixRepositorioMock.Setup(x => x.consultaPix(It.IsAny<Guid>())).Returns(pix);

            var mock = pixRepositorioMock.Object;

            //Act
            var saldo = mock.consultaPix(guid).Saldo;

            //Assert
            Assert.Equal(10, saldo);
        }
    }
}

