using System;
using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Util;

namespace InsideAirbnbApp.Repositories
{
    public interface IRepository<T>
    {
        public Task<T> Get(int id);
        public IQueryable<T> All();
        public IQueryable<T> Filter(Filter filters);
    }
}
