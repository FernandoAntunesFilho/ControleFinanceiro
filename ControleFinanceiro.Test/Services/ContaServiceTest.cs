using ControleFinanceiro.Models.DTOs;
using ControleFinanceiro.Models.Entities;
using ControleFinanceiro.Repositories;
using ControleFinanceiro.Services;
using Moq;

namespace ControleFinanceiro.Test.Services
{
    public class ContaServiceTest
    {
        private readonly Mock<IContaRepository> _repositoryMock;
        private readonly ContaService _service;

        public ContaServiceTest()
        {
            _repositoryMock = new Mock<IContaRepository>();
            _service = new ContaService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarContaComSucesso()
        {
            // Arrange
            var adicionadoComSucesso = 1;
            var request = new ContaRequestDTO
            {
                Nome = "Nova Conta",
                ValorInicial = 100,
                Ativo = true
            };
            _repositoryMock.Setup(r => r.Add(It.IsAny<Conta>())).ReturnsAsync(adicionadoComSucesso);

            // Act
            var result = await _service.AddAsync(request);

            // Assert
            Assert.Equal(adicionadoComSucesso, result);
            _repositoryMock.Verify(r => r.Add(It.IsAny<Conta>()), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task AddAsync_NomeNuloOuVazioOuEmBrancoDeveLancarExcessao(string nome)
        {
            // Arrange
            var mensagemExceptionEsperada = "O nome da conta é obrigatório.";
            var request = new ContaRequestDTO
            {
                Nome = nome
            };
            _repositoryMock.Setup(r => r.Add(It.IsAny<Conta>())).ReturnsAsync(1);

            // Act
            var resultException = await Assert.ThrowsAsync<Exception>(() => _service.AddAsync(request));

            // Assert
            Assert.Equal(mensagemExceptionEsperada, resultException.Message);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodasAsContas()
        {
            // Arrange
            var contasEsperadas = new List<Conta>
            {
                new Conta
                {
                    Id = 1,
                    Nome = "Conta A",
                    ValorInicial = 100,
                    Ativo = true
                },
                new Conta
                {
                    Id = 2,
                    Nome = "Conta B",
                    ValorInicial = 0,
                    Ativo = false
                }
            };
            _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(contasEsperadas);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(contasEsperadas, result);
            _repositoryMock.Verify(r => r.GetAll(), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task UpdateAsync_NomeNuloOuVazioOuEmBrancoDeveLancarExcessao(string nome)
        {
            // Arrange
            var mensagemExceptionEsperada = "O nome da conta é obrigatório.";
            var request = new ContaRequestDTO
            {
                Nome = nome
            };
            _repositoryMock.Setup(r => r.Update(It.IsAny<Conta>())).ReturnsAsync(1);

            // Act
            var resultException = await Assert.ThrowsAsync<Exception>(() => _service.AddAsync(request));

            // Assert
            Assert.Equal(mensagemExceptionEsperada, resultException.Message);
        }

        [Fact]
        public async Task UpdateAsync_ContaNaoEncontradaDeveLancaExcessao()
        {
            // Arrange
            var mensagemExceptionEsperada = "Conta não encontrada.";
            var contaId = 2;
            var conta = new Conta()
            {
                Id = 1,
                Nome = "Conta A",
                ValorInicial = 10,
                Ativo = true
            };
            var contas = new List<Conta>()
            {
                conta,
            };
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(contas.FirstOrDefault(c => c.Id == contaId));

            // Act
            var resultException = await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(conta));

            // Assert
            Assert.Equal(mensagemExceptionEsperada, resultException.Message);
        }

        [Fact]
        public async Task UpdateAsync_ContaExisteDeveAtualizarComSucesso()
        {
            // Arrange
            var contaAtualizada = 1;
            var contaId = 1;
            var conta = new Conta()
            {
                Id = 1,
                Nome = "Conta A",
                ValorInicial = 10,
                Ativo = true
            };
            var contaNova = new Conta()
            {
                Id = 1,
                Nome = "Conta B",
                ValorInicial = 0,
                Ativo = false
            };
            var contas = new List<Conta>()
            {
                conta,
            };
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(contas.FirstOrDefault(c => c.Id == contaId));
            _repositoryMock.Setup(r => r.Update(It.IsAny<Conta>())).ReturnsAsync(contaAtualizada);

            // Act
            var result = await _service.UpdateAsync(contaNova);

            // Assert
            Assert.Equal(contaAtualizada, result);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Conta>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ContaNaoEncontradaDeveLancaExcessao()
        {
            // Arrange
            var mensagemExceptionEsperada = "Conta não encontrada.";
            var contaId = 2;
            var conta = new Conta()
            {
                Id = 1,
                Nome = "Conta A",
                ValorInicial = 0,
                Ativo = true
            };
            var contas = new List<Conta>()
            {
                conta,
            };
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(contas.FirstOrDefault(c => c.Id == contaId));

            // Act
            var resultException = await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(contaId));

            // Assert
            Assert.Equal(mensagemExceptionEsperada, resultException.Message);
        }

        [Fact]
        public async Task DeleteAsync_ContaExisteDeveAtualizarComSucesso()
        {
            // Arrange
            var contaAtualizada = 1;
            var contaId = 1;
            var conta = new Conta()
            {
                Id = 1,
                Nome = "Conta A",
                ValorInicial = 0,
                Ativo = true
            };
            var contas = new List<Conta>()
            {
                conta,
            };
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(contas.FirstOrDefault(c => c.Id == contaId));
            _repositoryMock.Setup(r => r.Delete(It.IsAny<Conta>())).ReturnsAsync(contaAtualizada);

            // Act
            var result = await _service.DeleteAsync(contaId);

            // Assert
            Assert.Equal(contaAtualizada, result);
            _repositoryMock.Verify(r => r.Delete(It.IsAny<Conta>()), Times.Once);
        }
    }
}
