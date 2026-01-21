using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public class CategorieService : ICategorieService
    {
        private readonly ICategorieRepository _repositoryCategorie;

        public CategorieService(ICategorieRepository repository)
        {
            _repositoryCategorie = repository;
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

            var usedByArtikelen = await _repositoryCategorie.IsUsedByArtikelenAsync(id);

            if (usedByArtikelen)
                throw new InvalidOperationException(
            "Categorie kan niet worden verwijderd omdat zij wordt gebruikt door artikelen.");

            await _repositoryCategorie.DeleteAsync(categorie);
            await _repositoryCategorie.SaveChangesAsync();
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

            return allCategories
                .Where(c => c.HoofdCategorieId == null)
                .ToList();
        }

    }
}
