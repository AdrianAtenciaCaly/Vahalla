using Valhalla.Application.Interfaces;
using Valhalla.Domain.Entities;
using Valhalla.Infrastructure.Seed;

namespace Valhalla.Infrastructure.Repositories
{
    public class VikingRepository : IVikingRepository
    {
        private static readonly List<Viking> _vikings = VikingSeedData.GetData();

        public IEnumerable<Viking> GetAll()
        {
            return _vikings;
        }

        public Viking GetById(Guid id)
        {
            return _vikings.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Viking viking)
        {
            _vikings.Add(viking);
        }

        public void Update(Viking viking)
        {
            var current = _vikings.FirstOrDefault(x => x.Id == viking.Id);

            if (current == null)
                return;

            current.Name = viking.Name;

            current.BattlesWon =
                viking.BattlesWon;

            current.FavoriteWeapon =
                viking.FavoriteWeapon;

            current.HonorLevel =
                viking.HonorLevel;

            current.DeathCause =
                viking.DeathCause;

            current.ValhallaPoints =
                viking.ValhallaPoints;

            current.Category =
                viking.Category;

            current.IsApproved =
                viking.IsApproved;
        }

        public void Delete(Guid id)
        {
            var current = GetById(id);

            if (current != null)
                _vikings.Remove(current);
        }
    }
}
