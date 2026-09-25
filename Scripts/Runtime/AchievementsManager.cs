using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TelmanAchievements
{
    public class AchievementsManager : MonoBehaviour
    {
        public static AchievementsManager Instance { get; private set; }

        public static event Action<AchievementDefinition> AchievementUnlocked;

        private readonly HashSet<AchievementId> unlockedAchievements = new();

        [SerializeField] private AchievementDatabase database;

        private string SavePath => Path.Combine(
            Application.persistentDataPath,
            "TelmanAchievements",
            "PlayerData",
            "Achievements.json"
        );

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Load();
        }

        public void Unlock(AchievementId id)
        {
            if (!unlockedAchievements.Add(id))
                return;

            Save();

            AchievementDefinition achievement = database.Get(id);

            AchievementUnlocked?.Invoke(achievement);

            Debug.Log($"Achievement unlocked: {id}");
        }

        public bool IsUnlocked(AchievementId id)
        {
            return unlockedAchievements.Contains(id);
        }

        private void Save()
        {
            AchievementSaveData data = new();

            foreach (AchievementId id in unlockedAchievements)
            {
                data.unlockedAchievements.Add(id.ToString());
            }

            string directory = Path.GetDirectoryName(SavePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string json = JsonUtility.ToJson(data, true);

            File.WriteAllText(SavePath, json);
        }

        private void Load()
        {
            if (!File.Exists(SavePath))
                return;

            string json = File.ReadAllText(SavePath);
            AchievementSaveData data = JsonUtility.FromJson<AchievementSaveData>(json);

            if (data?.unlockedAchievements == null)
                return;

            foreach (string achievementName in data.unlockedAchievements)
            {
                if (Enum.TryParse(
                    achievementName,
                    out AchievementId id))
                {
                    unlockedAchievements.Add(id);
                }
            }
        }

        [ContextMenu("Clear All Achievements")]
        private void ClearAllAchievements()
        {
            unlockedAchievements.Clear();

            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }

            Debug.Log("All achievements cleared.");
        }
    }
}
