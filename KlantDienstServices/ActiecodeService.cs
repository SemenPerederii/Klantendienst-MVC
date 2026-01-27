using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices
{
    public class ActiecodeService : IActiecodeService
    {
        private readonly IActiecodeRepository _repositoryActiecode;
        private readonly ActiecodeRepository _repository;

        public ActiecodeService(IActiecodeRepository repository, ActiecodeRepository repo)
        {
            _repositoryActiecode = repository;
            _repository = repo;
        }

        public async Task<List<Actiecode>> GetAllActiecodesAsync()
        {
            return await _repositoryActiecode.GetAllAsync();
        }
        public async Task<Actiecode?> GetActiecodeByIdAsync(int id)
        {
            return await _repositoryActiecode.GetByIdAsync(id);
        }
    }
}
