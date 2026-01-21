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
        Task<List<Categorie>> GetAllCategorieAsync();
        List<Categorie> BuildTree(List<Categorie> allCategories);
        Task<Categorie?> GetCategorieAsync(int id);
    }
}
