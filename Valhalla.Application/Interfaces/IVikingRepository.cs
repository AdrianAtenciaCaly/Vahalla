using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valhalla.Domain.Entities;

namespace Valhalla.Application.Interfaces
{
    public interface IVikingRepository
    {
        IEnumerable<Viking> GetAll();

        Viking GetById(Guid id);

        void Add(Viking viking);

        void Update(Viking viking);

        void Delete(Guid id);
    }
}
