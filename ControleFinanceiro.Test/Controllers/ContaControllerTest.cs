using ControleFinanceiro.Controllers;
using ControleFinanceiro.Models.DTOs;
using ControleFinanceiro.Models.Entities;
using ControleFinanceiro.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControleFinanceiro.Test.Controllers
{
    public class ContaControllerTest
    {
        private readonly Mock<IContaService> _serviceMock;
        private readonly ContaController _controller;

        public ContaControllerTest()
        {
            _serviceMock = new Mock<IContaService>();
            _controller = new ContaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_RetornoDeveSerOKComListaDeContas()
        {
            // Arrange
            var contas = new List<Conta>
            {
                new Conta
                {
                    Id = 1,
                    Nome = "Conta A",
                    ValorInicial = 10,
                    Ativo = true
                }
            };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(contas);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Conta>>(okResult.Value);
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
            var contaCriada = 1;
            var novaConta = new ContaRequestDTO
            {
                Nome = "Nova Conta",
                ValorInicial = 10,
                Ativo = true
            };
            _serviceMock.Setup(s => s.AddAsync(It.IsAny<ContaRequestDTO>())).ReturnsAsync(contaCriada);

            // Act
            var result = await _controller.Add(novaConta);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Add_SeExcessaoDeveRetornarBadRequest()
        {
            // Arrange
            var novaConta = new ContaRequestDTO
            {
                Nome = "Nova Conta",
                ValorInicial = 10,
                Ativo = true
            };
            _serviceMock.Setup(s => s.AddAsync(It.IsAny<ContaRequestDTO>())).ThrowsAsync(new Exception("Erro"));

            // Act
            var result = await _controller.Add(novaConta);

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
            var contaAtualizada = 1;
            var novaConta = new Conta
            {
                Id = 1,
                Nome = "Nova Conta",
                ValorInicial = 10,
                Ativo = true
            };
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<Conta>())).ReturnsAsync(contaAtualizada);

            // Act
            var result = await _controller.Update(novaConta);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<int>(okResult.Value);
            Assert.Equal(contaAtualizada, returnValue);
        }

        [Fact]
        public async Task Update_SeExcessaoDeveRetornarBadRequest()
        {
            // Arrange
            var novaConta = new Conta
            {
                Id = 1,
                Nome = "Nova Conta",
                ValorInicial = 10,
                Ativo = true
            };
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<Conta>())).ThrowsAsync(new Exception("Erro"));

            // Act
            var result = await _controller.Update(novaConta);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value;
            var message = value?.GetType().GetProperty("message")?.GetValue(value)?.ToString();
            Assert.Equal("Erro", message);
        }

        [Fact]
        public async Task Delete_RetornoDeveSerNocontent()
        {
            // Arrange
            var contaApagada = 1;
            var contaId = 2;
            _serviceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(contaApagada);

            // Act
            var result = await _controller.Delete(contaId);

            // Assert
            var okResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_SeExcessaoDeveRetornarBadRequest()
        {
            // Arrange
            var contaId = 2;
            _serviceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Erro"));

            // Act
            var result = await _controller.Delete(contaId);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var value = badRequest.Value;
            var message = value?.GetType().GetProperty("message")?.GetValue(value)?.ToString();
            Assert.Equal("Erro", message);
        }
    }
}
