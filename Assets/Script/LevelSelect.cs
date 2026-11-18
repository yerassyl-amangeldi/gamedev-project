using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    public TextMeshProUGUI bestScoreText;

    private void OnEnable()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool unlocked = i + 1 <= SaveSystem.Data.unlockedLevel;
            levelButtons[i].interactable = unlocked;
            levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = unlocked ? "LEVEL " + (i + 1) : "LOCKED";
        }

        if (bestScoreText != null) bestScoreText.text = "BEST SCORE: " + SaveSystem.Data.bestScore;
    }

    public void LoadLevel(int number)
    {
        SceneManager.LoadScene("Level" + number);
    }
}
