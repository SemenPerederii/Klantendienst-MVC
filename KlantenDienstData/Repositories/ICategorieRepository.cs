using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstData.Repositories
{
    public interface ICategorieRepository
    {
        Task<List<Categorie>> GetAll();
        Task<Categorie> GetByIdAsync(int id);
        Task DeleteAsync(Categorie categorie);
        Task SaveChangesAsync();
        Task<bool> HasChildrenAsync(int id);
        Task<bool> IsUsedByArtikelenAsync(int id);
    }
}
