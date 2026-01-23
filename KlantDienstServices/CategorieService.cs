using KlantenDienstData.Models;
using KlantenDienstData.Repositories;

namespace KlantenDienstServices
{
    public class CategorieService : ICategorieService
    {
        private readonly ICategorieRepository _repositoryCategorie;
        private readonly CategorieRepository _repository;

        public CategorieService(ICategorieRepository repository, CategorieRepository repo)
        {
            _repositoryCategorie = repository;
            _repository = repo;
        }

        public async Task<IEnumerable<Categorie>> GetAllCategorieAsync()
        {
            return await _repositoryCategorie.GetAll();
        }

        public IEnumerable<Categorie> BuildTree(IEnumerable<Categorie> allCategories)
        {
            var lookup = allCategories.ToLookup(c => c.HoofdCategorieId);

            foreach (var categorie in allCategories)
            {
                categorie.InversehoofdCategorie = lookup[categorie.CategorieId].ToList();
            }

            return lookup[null].ToList();
        }

        public async Task<IEnumerable<Categorie>> GetHoofdcategorieenAsync()
        {
            return await _repositoryCategorie.HoofdcategorieAsync();
        }

        public async Task<Categorie?> GetCategorieByIdAsync(int id)
        {
            return await _repositoryCategorie.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Categorie>> GetSubcategorieenAsync(int hoofdCategorieId)
        {
            return await _repositoryCategorie.SubcategorieenAsync(hoofdCategorieId);
        }

        public async Task<bool> CategorieBestaatAlAsync(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam))
                return false;
            return await _repository.CategorieBestaatAlAsync(naam);
        }

        public async Task MaakHoofdcategorieAsync(Categorie nieuweCategorie, IEnumerable<int>? subCategorieIds)
        {
            if (nieuweCategorie == null)
                throw new ArgumentNullException(nameof(nieuweCategorie));

            await _repositoryCategorie.AddCategorieAsync(nieuweCategorie, subCategorieIds);
            
        }
    }
}
