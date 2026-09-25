using System;
using UnityEngine;

namespace TelmanAchievements
{
    [Serializable]
    public class AchievementDefinition
    {
        [SerializeField] private string id;
        [SerializeField] private string displayName;
        [SerializeField][TextArea] private string description;
        [SerializeField] private Sprite icon;

        public string Id => id;
        public string DisplayName => displayName;
        public string Description => description;
        public Sprite Icon => icon;
    }
}