using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int sellerId)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == sellerId);
        }

        public async Task RemoveAsync(int sellerId)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(sellerId);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete this seller because he/she has sales");
            }
        }

        public async Task UpdateAsync(Seller sellerId)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == sellerId.Id);

            if (!hasAny)
            {
                throw new KeyNotFoundException("Id not found");
            }

            try
            {
                _context.Update(sellerId);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
