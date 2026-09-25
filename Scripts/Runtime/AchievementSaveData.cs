using System;
using System.Collections.Generic;

namespace TelmanAchievements
{
    [Serializable]
    public class AchievementSaveData
    {
        public List<string> unlockedAchievements = new();
    }
}