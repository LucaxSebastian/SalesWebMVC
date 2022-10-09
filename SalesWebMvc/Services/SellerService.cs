using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int sellerId)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == sellerId);
        }

        public void Remove(int sellerId)
        {
            var obj = _context.Seller.Find(sellerId);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller sellerId)
        {
            if (!_context.Seller.Any(x => x.Id == sellerId.Id))
            {
                throw new KeyNotFoundException("Id not found");
            }

            try
            {
                _context.Update(sellerId);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
