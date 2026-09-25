using TelmanAchievements;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame)
            AchievementsManager.Instance.Unlock(AchievementId.TimeToGo);

        if (Keyboard.current.lKey.wasPressedThisFrame)
            AchievementsManager.Instance.Unlock(AchievementId.CuriosNose);
    }
}
