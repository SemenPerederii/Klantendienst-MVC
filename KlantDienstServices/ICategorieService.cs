using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public interface ICategorieService
    {
        Task<IEnumerable<Categorie>> GetAllCategorieAsync();
        IEnumerable<Categorie> BuildTree(IEnumerable<Categorie> allCategories);
        Task DeleteCategorieAsync(int id);
        Task<IEnumerable<Categorie>> GetMogelijkeCategorieenAsync(int categorieId);
        Task AddAsSubcategorieAsync(int categorieId, string naam, int? newParentId);
        Task<Categorie> GetByIdAsync(int id);
    }
}
