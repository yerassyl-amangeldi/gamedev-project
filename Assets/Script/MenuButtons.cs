using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public AudioSource bgSound;
    public GameObject settingsPanel;


    public void Play()
    {
        SceneManager.LoadScene("Level1"); 
    }

     public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting the game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void Back()
    {
        settingsPanel.SetActive(false);
    }

    public void SetVolume(float value)
    {
        bgSound.volume = value;
        PlayerPrefs.SetFloat("Volume", value); 
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

}