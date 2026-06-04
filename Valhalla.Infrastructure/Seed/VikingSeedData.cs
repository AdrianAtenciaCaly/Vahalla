using Valhalla.Domain.Entities;
using Valhalla.Domain.Enums;

namespace Valhalla.Infrastructure.Seed
{
    public static class VikingSeedData
    {
        public static List<Viking> GetData()
        {
            return new List<Viking>
            {
                new Viking
                {
                    Id = Guid.NewGuid(),
                    Name = "Ragnar Lothbrok",
                    BattlesWon = 95,
                    FavoriteWeapon = "Hacha",
                    HonorLevel = HonorLevel.High,
                    DeathCause = "Serpientes",
                    ValhallaPoints = 575,
                    Category = "Guerrero de Thor",
                    IsApproved = true
                },

                new Viking
                {
                    Id = Guid.NewGuid(),
                    Name = "Bjorn Ironside",
                    BattlesWon = 70,
                    FavoriteWeapon = "Espada",
                    HonorLevel = HonorLevel.High,
                    DeathCause = "Batalla",
                    ValhallaPoints = 450,
                    Category = "Guerrero de Freyja",
                    IsApproved = true
                },

                new Viking
                {
                    Id = Guid.NewGuid(),
                    Name = "Ivar The Boneless",
                    BattlesWon = 45,
                    FavoriteWeapon = "Lanza",
                    HonorLevel = HonorLevel.Medium,
                    DeathCause = "Desconocida",
                    ValhallaPoints = 275,
                    Category = "Guerrero de Freyja",
                    IsApproved = true
                },

                new Viking
                {
                    Id = Guid.NewGuid(),
                    Name = "Ubbe Ragnarsson",
                    BattlesWon = 25,
                    FavoriteWeapon = "Escudo",
                    HonorLevel = HonorLevel.Medium,
                    DeathCause = "Exploración",
                    ValhallaPoints = 175,
                    Category = "Guerrero de Odín",
                    IsApproved = true
                },

                new Viking
                {
                    Id = Guid.NewGuid(),
                    Name = "Floki",
                    BattlesWon = 10,
                    FavoriteWeapon = "Martillo",
                    HonorLevel = HonorLevel.Low,
                    DeathCause = "Naufragio",
                    ValhallaPoints = 60,
                    Category = "Guerrero de Odín",
                    IsApproved = false
                }
            };
        }
    }
}