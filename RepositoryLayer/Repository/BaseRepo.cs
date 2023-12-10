using DomainLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLayer.Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly MyContext context;

        public BaseRepo(MyContext context)
        {
            this.context = context;
        }

        public void ADD(T item)
        {
            context.Add(item);
            context.SaveChanges();
            
        }

        public void DELETE(string id)
        {
          context.Remove(context.Set<T>());
            context.SaveChanges();  
        }

        public List<T> GettAll()
        {
            return context.Set<T>().ToList();
        }

        public T GettById(int id)
        {
            return context.Set<T>().Find(id);  
        }

        public void UPDATE(T item, int id)
        {
            GettById(id);
            if (item == null) {

                throw new ArgumentException($"Entity with ID {id} not found.");

            }


            context.SaveChanges();
        }
    }
}
