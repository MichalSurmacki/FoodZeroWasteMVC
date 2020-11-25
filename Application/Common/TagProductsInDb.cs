using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Common
{
    public class TagProductsInDb
    {
        private readonly IApplicationDbContext _context;

        public TagProductsInDb(IApplicationDbContext context)
        {
            _context = context;
        }

        public void TagProducts()
        {
            var products = _context.Products.ToList();

            foreach(Product p in products)
            {
                var tags = Morfeusz.Morfeusz2.GetAllUniqueTags(p.Name);
                foreach(string s in tags)
                {
                    var t = new Tag();
                    t.Value = s;
                    t.Product = p;
                    _context.Tags.Add(t);
                }
            }
            _context.SaveChanges();
        }
    }
}
