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

        public async Task<Categorie> GetByIdAsync(int id)
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

                if (await _repositoryCategorie.HasArtikelenAsync(categorieId))
                    throw new InvalidOperationException(
                        "Categorie met artikelen kan niet worden verplaatst.");

                categorie.HoofdCategorieId = newParentId;
            }

            await _repositoryCategorie.SaveChangesAsync();
        }
    }
}
