using Valhalla.Domain.Entities;

namespace Valhalla.Application.Interfaces
{
    public interface IVikingClassificationService
    {
        void Classify(Viking viking);
    }
}
