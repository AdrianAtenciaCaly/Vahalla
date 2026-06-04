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
