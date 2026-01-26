using KlantenDienstData.Models;

namespace KlantenDienstData.Repositories
{
    public interface ICategorieRepository
    {
        Task<List<Categorie>> GetAll();
        Task<Categorie> GetByIdAsync(int id);
        Task DeleteAsync(Categorie categorie);
        Task SaveChangesAsync();
        Task<bool> HasChildrenAsync(int id);
        Task<bool> HasArtikelenAsync(int id);
        Task<Categorie?> GetCategorieAsync(int id);
        Task<Categorie?> GetByIdAsync(int id);
        Task<List<Categorie>> GetByIdsAsync(IEnumerable<int> ids);
        Task<Categorie?> AddCategorieAsync(Categorie categorie, IEnumerable<int> subCategorieIds);
        Task<IEnumerable<Categorie>> HoofdcategorieAsync();
        Task<bool> CategorieBestaatAlAsync(string naam);
        Task<IEnumerable<Categorie>> SubcategorieenAsync(int hoofdCategorieId);
        Task<Categorie?> AddSubcategorieAsync(Categorie categorie, int hoofdcategorieId, IEnumerable<int>? subCategorieIds);


    }
}