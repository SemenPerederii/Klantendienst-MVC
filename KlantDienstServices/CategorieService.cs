using KlantenDienstData.Models;
using KlantenDienstData.Repositories;

namespace KlantenDienstServices
{
    public class CategorieService
    {
        private readonly ICategorieRepository _repositoryCategorie;
        private readonly CategorieRepository _repository;

        public CategorieService(ICategorieRepository repository, CategorieRepository repo)
        {
            _repositoryCategorie = repository;
            _repository = repo;
        }

        public async Task<List<Categorie>> GetAllCategorieAsync()
        {
            return await _repositoryCategorie.GetAll();
        }

        public List<Categorie> BuildTree(List<Categorie> allCategories)
        {
            var lookup = allCategories.ToLookup(c => c.HoofdCategorieId);

            foreach (var categorie in allCategories)
            {
                categorie.InversehoofdCategorie = lookup[categorie.CategorieId].ToList();
            }

            return lookup[null].ToList();
        }
        public async Task<Categorie?> AddCategorieAsync(Categorie categorie)
        {
            return await _repositoryCategorie.AddCategorieAsync(categorie);
        }
        public async Task<bool> CategorieBestaatAlAsync(string naam)
        {
            return await _repository.CategorieBestaatAlAsync(naam);
        }
    }
}