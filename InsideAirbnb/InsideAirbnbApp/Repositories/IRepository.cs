using System.Collections.Generic;

namespace InsideAirbnbApp.Repositories
{
    public interface IRepository<T>
    {
        public T Get(int id);
        public T Get(string id);
        public IEnumerable<T> All();
    }
}
