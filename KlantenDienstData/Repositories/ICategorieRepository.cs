using KlantenDienstData.Models;

namespace KlantenDienstData.Repositories
{
    public interface ICategorieRepository
    {
        Task<List<Categorie>> GetAll();
        Task<Categorie?> AddCategorieAsync(Categorie categorie);
    }
}