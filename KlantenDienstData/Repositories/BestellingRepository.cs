using KlantenDienstData.Models;
using Microsoft.EntityFrameworkCore;

namespace KlantenDienstData.Repositories;
public class BestellingRepository : IBestellingRepository
{
    private readonly PrulariaDbContext _context;
    public BestellingRepository(PrulariaDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Bestelling>> GetAllBestellingenAsync()
    {
        return await _context.Bestellingen
        .Include(b => b.Klant)
            .ThenInclude(k => k.Rechtspersonen)
                .ThenInclude(rp => rp.Contactpersonen)
        .Include(b => b.BestellingsStatus)
        .Include(b => b.FacturatieAdres)
            .ThenInclude(a => a.Plaats)
        .Include(b => b.LeveringsAdres)
            .ThenInclude(a => a.Plaats)
        .ToListAsync();
    }

    public async Task<Bestelling?> GetBestellingAsync(int id)
    {
        throw new NotImplementedException();
    }
}