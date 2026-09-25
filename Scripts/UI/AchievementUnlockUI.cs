using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TelmanAchievements.UI
{
    public class AchievementUnlockUI : MonoBehaviour
    {
        [SerializeField] private GameObject achievementPanel;
        [SerializeField] private TMP_Text achievementName;
        [SerializeField] private Image achievementImage;

        private Coroutine hideCoroutine;

        private void Awake()
        {
            achievementPanel.SetActive(false);
        }

        private void OnEnable()
        {
            AchievementsManager.AchievementUnlocked += OnAchievementUnlocked;
        }

        private void OnDisable()
        {
            AchievementsManager.AchievementUnlocked -= OnAchievementUnlocked;
        }

        private void OnAchievementUnlocked(AchievementDefinition definition)
        {
            achievementName.text = definition.DisplayName;
            achievementImage.sprite = definition.Icon;

            achievementPanel.SetActive(true);

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }

            hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(3f);

            achievementPanel.SetActive(false);
            hideCoroutine = null;
        }
    }
}