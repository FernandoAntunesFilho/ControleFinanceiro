using ControleFinanceiro.DTOs;
using ControleFinanceiro.Models;
using ControleFinanceiro.Repositories;
using ControleFinanceiro.Services;
using Moq;

namespace ControleFinanceiro.Test.Services
{
    public class CategoriaServiceTest
    {
        private readonly Mock<ICategoriaRepository> _repositoryMock;
        private readonly CategoriaService _service;

        public CategoriaServiceTest()
        {
            _repositoryMock = new Mock<ICategoriaRepository>();
            _service = new CategoriaService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarCategoriaComSucesso()
        {
            // Arrange
            var adicionadoComSucesso = 1;
            var request = new CategoriaRequestDTO
            {
                Nome = "Nova Categoria"
            };
            _repositoryMock.Setup(r => r.Add(It.IsAny<Categoria>())).ReturnsAsync(adicionadoComSucesso);

            // Act
            var result = await _service.AddAsync(request);

            // Assert
            Assert.Equal(adicionadoComSucesso, result);
            _repositoryMock.Verify(r => r.Add(It.IsAny<Categoria>()), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task AddAsync_NomeNuloOuVazioOuEmBrancoDeveLancarExcessao(string nome)
        {
            // Arrange
            var mensagemExceptionEsperada = "O nome da categoria é obrigatório.";
            var request = new CategoriaRequestDTO
            {
                Nome = nome
            };
            _repositoryMock.Setup(r => r.Add(It.IsAny<Categoria>())).ReturnsAsync(1);

            // Act
            var resultException = await Assert.ThrowsAsync<Exception>(() => _service.AddAsync(request));

            // Assert
            Assert.Equal(mensagemExceptionEsperada, resultException.Message);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodasAsCategorias()
        {
            // Arrange
            var categoriasEsperadas = new List<Categoria>
            {
                new Categoria
                {
                    Id = 1,
                    Nome = "Categoria 1"
                },
                new Categoria
                {
                    Id = 2,
                    Nome = "Categoria 2"
                }
            };
            _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(categoriasEsperadas);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(categoriasEsperadas, result);
            _repositoryMock.Verify(r => r.GetAll(), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task UpdateAsync_NomeNuloOuVazioOuEmBrancoDeveLancarExcessao(string nome)
        {
            // Arrange
            var mensagemExceptionEsperada = "O nome da categoria é obrigatório.";
            var request = new CategoriaRequestDTO
            {
                Nome = nome
            };
            _repositoryMock.Setup(r => r.Update(It.IsAny<Categoria>())).ReturnsAsync(1);

            // Act
            var resultException = await Assert.ThrowsAsync<Exception>(() => _service.AddAsync(request));

            // Assert
            Assert.Equal(mensagemExceptionEsperada, resultException.Message);
        }

        [Fact]
        public async Task UpdateAsync_CategoriaNaoEncontradaDeveLancaExcessao()
        {
            // Arrange
            var mensagemExceptionEsperada = "Categoria não encontrada.";
            var categoriaId = 2;
            var categoria = new Categoria()
            {
                Id = 1,
                Nome = "Categoria"
            };
            var categorias = new List<Categoria>()
            {
                categoria,
            };
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(categorias.FirstOrDefault(c =>  c.Id == categoriaId));

            // Act
            var resultException = await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(categoria));

            // Assert
            Assert.Equal(mensagemExceptionEsperada, resultException.Message);
        }

        [Fact]
        public async Task UpdateAsync_CategoriaExisteDeveAtualizarComSucesso()
        {
            // Arrange
            var categoriaAtualizada = 1;
            var categoriaId = 1;
            var categoria = new Categoria()
            {
                Id = 1,
                Nome = "Categoria"
            };
            var categoriaNova = new Categoria()
            {
                Id = 1,
                Nome = "Categoria Nova"
            };
            var categorias = new List<Categoria>()
            {
                categoria,
            };
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(categorias.FirstOrDefault(c => c.Id == categoriaId));
            _repositoryMock.Setup(r => r.Update(It.IsAny<Categoria>())).ReturnsAsync(categoriaAtualizada);

            // Act
            var result = await _service.UpdateAsync(categoriaNova);

            // Assert
            Assert.Equal(categoriaAtualizada, result);
            _repositoryMock.Verify(r => r.Update(It.IsAny<Categoria>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CategoriaNaoEncontradaDeveLancaExcessao()
        {
            // Arrange
            var mensagemExceptionEsperada = "Categoria não encontrada.";
            var categoriaId = 2;
            var categoria = new Categoria()
            {
                Id = 1,
                Nome = "Categoria"
            };
            var categorias = new List<Categoria>()
            {
                categoria,
            };
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(categorias.FirstOrDefault(c => c.Id == categoriaId));

            // Act
            var resultException = await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(categoriaId));

            // Assert
            Assert.Equal(mensagemExceptionEsperada, resultException.Message);
        }

        [Fact]
        public async Task DeleteAsync_CategoriaExisteDeveAtualizarComSucesso()
        {
            // Arrange
            var categoriaAtualizada = 1;
            var categoriaId = 1;
            var categoria = new Categoria()
            {
                Id = 1,
                Nome = "Categoria"
            };
            var categorias = new List<Categoria>()
            {
                categoria,
            };
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(categorias.FirstOrDefault(c => c.Id == categoriaId));
            _repositoryMock.Setup(r => r.Delete(It.IsAny<Categoria>())).ReturnsAsync(categoriaAtualizada);

            // Act
            var result = await _service.DeleteAsync(categoriaId);

            // Assert
            Assert.Equal(categoriaAtualizada, result);
            _repositoryMock.Verify(r => r.Delete(It.IsAny<Categoria>()), Times.Once);
        }
    }
}
