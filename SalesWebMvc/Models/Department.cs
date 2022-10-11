using System.Collections.Generic;
using System.Linq;
using System;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {
        }

        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public double TotalSales(DateTime inital, DateTime final)
        {
            return Sellers.Sum(saller => saller.TotalSales(inital, final));
        }
    }
}
