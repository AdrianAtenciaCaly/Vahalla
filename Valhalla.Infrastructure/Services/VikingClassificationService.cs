using Valhalla.Application.Interfaces;
using Valhalla.Domain.Entities;
using Valhalla.Domain.Enums;

namespace Valhalla.Infrastructure.Services
{
    public class VikingClassificationService : IVikingClassificationService
    {
        public void Classify(Viking viking)
        {
            viking.ValhallaPoints = CalculatePoints(viking);

            viking.Category = GetCategory(viking);

            viking.IsApproved = viking.ValhallaPoints >= 120;
        }

        private int CalculatePoints(Viking viking)
        {
            int points = viking.BattlesWon * 5;

            switch (viking.HonorLevel)
            {
                case HonorLevel.High:
                    points += 100;
                    break;

                case HonorLevel.Medium:
                    points += 50;
                    break;

                case HonorLevel.Low:
                    points += 10;
                    break;
            }

            return points;
        }

        private string GetCategory(Viking viking)
        {
            if (viking.BattlesWon >= 80)
                return "Guerrero de Thor";

            if (viking.BattlesWon >= 40)
                return "Guerrero de Freyja";

            return "Guerrero de Odín";
        }
    }
}
