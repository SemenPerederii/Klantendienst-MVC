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

        public async Task<List<Categorie>> GetAllCategorie()
        {
            return await _repositoryCategorie.GetAll();
        }
    }
}
