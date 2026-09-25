using System.Collections.Generic;
using UnityEngine;

namespace TelmanAchievements
{

    [CreateAssetMenu(menuName = "Game/Achievement Database")]
    public class AchievementDatabase : ScriptableObject
    {
        [SerializeField] private List<AchievementDefinition> achievements = new();

        public IReadOnlyList<AchievementDefinition> Achievements => achievements;

        public AchievementDefinition Get(AchievementId id)
        {
            foreach (AchievementDefinition achievement in achievements)
            {
                if (achievement.Id == id.ToString())
                    return achievement;
            }

            return null;
        }
    }
}