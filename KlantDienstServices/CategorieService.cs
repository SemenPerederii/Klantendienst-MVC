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

        public async Task DeleteCategorieAsync(int id)
        {
            var categorie = await _repositoryCategorie.GetByIdAsync(id);

            if (categorie == null)
                throw new Exception("Categorie niet gevonden");

            var hasChildren = await _repositoryCategorie.HasChildrenAsync(id);

            if (hasChildren)
                throw new InvalidOperationException(
                    "Categorie kan niet worden verwijderd omdat zij subcategorieën heeft.");

            var hasArtikelen = await _repositoryCategorie.HasArtikelenAsync(id);

            if (hasArtikelen)
                throw new InvalidOperationException(
            "Categorie kan niet worden verwijderd omdat zij wordt gebruikt door artikelen.");

            await _repositoryCategorie.DeleteAsync(categorie);
            await _repositoryCategorie.SaveChangesAsync();
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

            return allCategories
                .Where(c => c.HoofdCategorieId == null)
                .ToList();
        }

        public async Task<Categorie?> GetByIdAsync(int id)
        {
            return await _repositoryCategorie.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Categorie>> GetMogelijkeCategorieenAsync(int categorieId)
        {
            var all = await _repositoryCategorie.GetAll();

            return all
                .Where(c => c.CategorieId != categorieId);
        }

        public async Task AddAsSubcategorieAsync(int categorieId, string naam, int? newParentId)
        {
            var categorie = await _repositoryCategorie.GetByIdAsync(categorieId);

            if (categorie == null)
                throw new Exception("Categorie niet gevonden");

            //Naam
            if (string.IsNullOrWhiteSpace(naam))
                throw new InvalidOperationException("Naam is verplicht");

            categorie.Naam = naam.Trim();

            //Structure
            if (newParentId != categorie.HoofdCategorieId)
            {
                if (newParentId == categorieId)
                    throw new InvalidOperationException(
                        "Categorie kan niet aan zichzelf worden gekoppeld.");

                if (await _repositoryCategorie.HasChildrenAsync(categorieId))
                    throw new InvalidOperationException(
                        "Categorie met subcategorieën kan niet worden verplaatst.");

                categorie.HoofdCategorieId = newParentId;
            }
            await _repositoryCategorie.SaveChangesAsync();
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

        public async Task MaakSubcategorieAsync(int hoofdCategorieId, Categorie nieuweSubcategorie, IEnumerable<int>? subCategorieIds)
        {
            if (nieuweSubcategorie == null)
                throw new ArgumentNullException(nameof(nieuweSubcategorie));
            await _repositoryCategorie.AddSubcategorieAsync(nieuweSubcategorie, hoofdCategorieId, subCategorieIds);
        }
            
        public Task<Categorie?> GetCategorieAsync(int id)
        {
            return _repositoryCategorie.GetCategorieAsync(id);
        }      
    }
}
