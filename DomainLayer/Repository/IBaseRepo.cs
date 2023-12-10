using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IBaseRepo<T>where T : class
    {
        List<T> GettAll();
        T GettById(int id);
        void ADD(T item);
        void UPDATE(T item,int id);
        void DELETE(string id);
       
        

    }
}
