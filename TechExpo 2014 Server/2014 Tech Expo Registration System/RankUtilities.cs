#region

using System;
using System.Linq;
using TechExpo.Data.Models;

#endregion

namespace _2014_Tech_Expo_Registration_System
{
    public class RankUtilities
    {
        public enum Rank
        {
            ENSIGN = 0,
            LIEUTENANT = 1000,
            COMMANDER = 2000,
            CAPTAIN = 3000
        }

        public static int PointsToNextRank(Rank currentRank, int currentPoints)
        {
            if (currentRank == Rank.CAPTAIN)
            {
                return 0;
            }

            var nextRank = GetNextRank(currentRank);

            var result = (int) nextRank - currentPoints;

            return result;
        }

        public static Rank GetNextRank(Rank currentRank)
        {
            var nextRank = Enum.GetValues(typeof (Rank))
                               .Cast<Rank>()
                               .FirstOrDefault(r => (int) r > (int) currentRank);

            if (nextRank == null)
            {
                nextRank = Rank.CAPTAIN;
            }

            return nextRank;
        }

        public static Rank GetRank(Registrant registrant)
        {
            Rank result;
            var total = registrant.Participations.Sum(participation => participation.Location.PointsValue);

            if (total >= 1000 && total <= 1900)
            {
                result = Rank.LIEUTENANT;
            }
            else if (total >= 2000 && total <= 2900)
            {
                result = Rank.COMMANDER;
            }
            else if (total >= 3000)
            {
                result = Rank.CAPTAIN;
            }
            else
            {
                result = Rank.ENSIGN;
            }

            return result;
        }
    }
}