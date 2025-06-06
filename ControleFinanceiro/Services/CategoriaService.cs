using ControleFinanceiro.DTOs;
using ControleFinanceiro.Models;
using ControleFinanceiro.Repositories;

namespace ControleFinanceiro.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private const string CategoriaNotFoundMessage = "Categoria não encontrada.";
        private const string CategoriaNameRequiredMessage = "O nome da categoria é obrigatório.";

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddAsync(CategoriaRequestDTO categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome)) throw new Exception(CategoriaNameRequiredMessage);

            var novaCategoria = new Categoria
            {
                Nome = categoria.Nome
            };

            return await _repository.Add(novaCategoria);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var categoria = await _repository.GetById(id);
            if (categoria is null) throw new Exception(CategoriaNotFoundMessage);
            
            return await _repository.Delete(categoria);
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<int> UpdateAsync(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nome)) throw new Exception(CategoriaNameRequiredMessage);

            var categoriaAtual = await _repository.GetById(categoria.Id);
            if (categoriaAtual is null) throw new Exception(CategoriaNotFoundMessage);

            categoriaAtual.Nome = categoria.Nome;
            return await _repository.Update(categoriaAtual);
        }
    }
}
