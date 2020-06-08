using System.Linq;
using System.Threading.Tasks;
using InsideAirbnbApp.Util;
using InsideAirbnbApp.ViewModels;

namespace InsideAirbnbApp.Repositories
{
    public interface IRepository<T>
    {
        public Task<T> Get(int id);
        public IQueryable<T> All();
        public IQueryable<T> Filter(Filter filters);
        public IQueryable<T> Join(IQueryable<ListingsViewModel> query);
    }
}
