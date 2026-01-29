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
        Task<Categorie?> GetCategorieAsync(int id);
        Task<IEnumerable<Categorie>> GetAllCategorieAsync();
        Task<Categorie?> GetCategorieByIdAsync(int id);
        Task<bool> CategorieBestaatAlAsync(string naam);
        IEnumerable<Categorie> BuildTree(IEnumerable<Categorie> allCategories);
        Task<IEnumerable<Categorie>> GetHoofdcategorieenAsync();
        Task<IEnumerable<Categorie>> GetSubcategorieenAsync(int hoofdCategorieId);
        Task MaakHoofdcategorieAsync(Categorie nieuweCategorie, IEnumerable<int> subCategorieIds);
        Task MaakSubcategorieAsync(int hoofdCategorieId, Categorie nieuweSubcategorie, IEnumerable<int>? subCategorieIds);
        Task DeleteCategorieAsync(int id);
        Task<IEnumerable<Categorie>> GetMogelijkeCategorieenAsync(int categorieId);
        Task AddAsSubcategorieAsync(int categorieId, string naam, int? newParentId);
        Task<Categorie> GetByIdAsync(int id);
        Task<IEnumerable<Categorie>> GetAllCategorieAsync(ArtikelFilterDto? filters);
    }
}
