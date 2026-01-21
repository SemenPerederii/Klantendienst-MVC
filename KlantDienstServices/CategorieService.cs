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

        public Task<Categorie?> GetCategorieAsync(int id)
        {
            return _repositoryCategorie.GetCategorieAsync(id);
        }
    }
}
