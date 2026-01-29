using KlantenDienstData.Enums;
using KlantenDienstData.Models;
using KlantenDienstData.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public ActiecodeService(IActiecodeRepository repository)
        {
            _repositoryActiecode = repository;
        }

        public async Task<IEnumerable<Actiecode>> GetAllActiecodesAsync()
        {
            return await _repositoryActiecode.GetAllAsync();
        }

        public async Task<IEnumerable<Actiecode>> GetAllActiecodesAsync(ActiecodeStatus filter,
                            DateOnly? datum, ActiecodeSorteerOpties sorteerOpties, SorteerRichting sorteerRichting)
        {

            IQueryable<Actiecode> query = _repositoryActiecode.GetActiecodeQuery();

            var vandaag = DateOnly.FromDateTime(DateTime.Today);

            //filter

            query = filter switch
            {
                ActiecodeStatus.Actief =>
                    query.Where(a => a.GeldigVanDatum <= vandaag &&
                                     a.GeldigTotDatum >= vandaag),

                ActiecodeStatus.NietActief =>
                    query.Where(a => a.GeldigVanDatum > vandaag ||
                                     a.GeldigTotDatum < vandaag),

                ActiecodeStatus.ExacteDatum when datum.HasValue =>
                    query.Where(a => a.GeldigVanDatum <= datum.Value &&
                                     a.GeldigTotDatum >= datum.Value),

                _ => query
            };

            //sorteren

            query = (sorteerOpties, sorteerRichting) switch
            {
                (ActiecodeSorteerOpties.Naam, SorteerRichting.Asc) =>
                    query.OrderBy(a => a.Naam),

                (ActiecodeSorteerOpties.Naam, SorteerRichting.Desc) =>
                    query.OrderByDescending(a => a.Naam),

                (ActiecodeSorteerOpties.GeldigVanDatum, SorteerRichting.Asc) =>
                    query.OrderBy(a => a.GeldigVanDatum),

                (ActiecodeSorteerOpties.GeldigVanDatum, SorteerRichting.Desc) =>
                    query.OrderByDescending(a => a.GeldigVanDatum),

                (ActiecodeSorteerOpties.GeldigTotDatum, SorteerRichting.Asc) =>
                    query.OrderBy(a => a.GeldigTotDatum),

                (ActiecodeSorteerOpties.GeldigTotDatum, SorteerRichting.Desc) =>
                    query.OrderByDescending(a => a.GeldigTotDatum),

                _ => query.OrderBy(a => a.Naam)
            };

            return await query.ToListAsync();

        }
        public async Task<Actiecode?> GetActiecodeByIdAsync(int id)
        {
            return await _repositoryActiecode.GetByIdAsync(id);
        }
        public async Task WijzigActieCode(int id, Actiecode nieuweActiecode)=>await _repositoryActiecode.WijzigActieCode(id,nieuweActiecode);
        public async Task Toevoegen(Actiecode actiecode)=> await _repositoryActiecode.Toevoegen(actiecode);

    }
}
