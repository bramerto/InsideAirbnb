﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnbApp.Repositories
{
    public interface IRepository<T>
    {
        public Task<T> Get(int id);
        public Task<T> Get(string id);
        public IQueryable<T> All();
    }
}
