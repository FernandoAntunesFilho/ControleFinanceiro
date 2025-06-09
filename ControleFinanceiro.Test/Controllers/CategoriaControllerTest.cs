using ControleFinanceiro.Controllers;
using ControleFinanceiro.DTOs;
using ControleFinanceiro.Models;
using ControleFinanceiro.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControleFinanceiro.Test.Controllers
{
    public class CategoriaControllerTest
    {
        private readonly Mock<ICategoriaService> _serviceMock;
        private readonly CategoriaController _controller;

        public CategoriaControllerTest()
        {
            _serviceMock = new Mock<ICategoriaService>();
            _controller = new CategoriaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_RetornoDeveSerOKComListaDeCategorias()
        {
            // Arrange
            var categorias = new List<Categoria>
            {
                new Categoria
                {
                    Id = 1,
                    Nome = "Categoria"
                }
            };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(categorias);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Categoria>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetAll_SeExcessaoDeveRetornarBadRequest()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAllAsync()).ThrowsAsync(new Exception("Erro"));

            // Act
            var result = await _controller.GetAll();

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value;
            var message = value?.GetType().GetProperty("message")?.GetValue(value)?.ToString();
            Assert.Equal("Erro", message);
        }

        [Fact]
        public async Task Add_RetornoDeveSerCreated()
        {
            // Arrange
            var categoriaCriada = 1;
            var novaCategoria = new CategoriaRequestDTO
            {
                Nome = "Nova Categoria"
            };
            _serviceMock.Setup(s => s.AddAsync(It.IsAny<CategoriaRequestDTO>())).ReturnsAsync(categoriaCriada);

            // Act
            var result = await _controller.Add(novaCategoria);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Add_SeExcessaoDeveRetornarBadRequest()
        {
            // Arrange
            var novaCategoria = new CategoriaRequestDTO
            {
                Nome = "Nova Categoria"
            };
            _serviceMock.Setup(s => s.AddAsync(It.IsAny<CategoriaRequestDTO>())).ThrowsAsync(new Exception("Erro"));

            // Act
            var result = await _controller.Add(novaCategoria);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value;
            var message = value?.GetType().GetProperty("message")?.GetValue(value)?.ToString();
            Assert.Equal("Erro", message);
        }

        [Fact]
        public async Task Update_RetornoDeveSerOk()
        {
            // Arrange
            var categoriaAtualizada = 1;
            var novaCategoria = new Categoria
            {
                Id = 1,
                Nome = "Nova Categoria"
            };
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<Categoria>())).ReturnsAsync(categoriaAtualizada);

            // Act
            var result = await _controller.Update(novaCategoria);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<int>(okResult.Value);
            Assert.Equal(categoriaAtualizada, returnValue);
        }

        [Fact]
        public async Task Update_SeExcessaoDeveRetornarBadRequest()
        {
            // Arrange
            var novaCategoria = new Categoria
            {
                Id = 1,
                Nome = "Nova Categoria"
            };
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<Categoria>())).ThrowsAsync(new Exception("Erro"));

            // Act
            var result = await _controller.Update(novaCategoria);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value;
            var message = value?.GetType().GetProperty("message")?.GetValue(value)?.ToString();
            Assert.Equal("Erro", message);
        }

        [Fact]
        public async Task Delete_RetornoDeveSerOk()
        {
            // Arrange
            var categoriaApagada = 1;
            var categoriaId = 2;
            _serviceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(categoriaApagada);

            // Act
            var result = await _controller.Delete(categoriaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<int>(okResult.Value);
            Assert.Equal(categoriaApagada, returnValue);
        }

        [Fact]
        public async Task Delete_SeExcessaoDeveRetornarBadRequest()
        {
            // Arrange
            var categoriaId = 2;
            _serviceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Erro"));

            // Act
            var result = await _controller.Delete(categoriaId);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value;
            var message = value?.GetType().GetProperty("message")?.GetValue(value)?.ToString();
            Assert.Equal("Erro", message);
        }
    }
}
