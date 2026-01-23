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
        Task<Categorie?> GetCategorieByIdAsync(int id);
        IEnumerable<Categorie> BuildTree(IEnumerable<Categorie> allCategories);
        Task<IEnumerable<Categorie>> GetHoofdcategorieenAsync();
        Task<IEnumerable<Categorie>> GetSubcategorieenAsync(int hoofdCategorieId);
        Task MaakHoofdcategorieAsync(Categorie nieuweCategorie, IEnumerable<int> subCategorieIds);
    }
}
