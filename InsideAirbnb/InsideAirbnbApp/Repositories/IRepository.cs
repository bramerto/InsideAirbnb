using System.Collections.Generic;
using System.Linq;

namespace InsideAirbnbApp.Repositories
{
    public interface IRepository<T>
    {
        public T Get(int id);
        public T Get(string id);
        public IQueryable<T> All();
    }
}
