using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileSettings : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_Dropdown colorDropdown;
    public Toggle fullscreenToggle;
    public Toggle musicToggle;
    public Slider volumeSlider;
    public AudioSource bgSound;

    void Start()
    {
        SaveData data = SaveSystem.Data;
        nameField.text = data.playerName;
        colorDropdown.value = data.playerColor;
        fullscreenToggle.isOn = data.fullscreen;
        musicToggle.isOn = data.music;
        volumeSlider.value = data.volume;
        bgSound.mute = !data.music;
        bgSound.volume = data.volume;
    }

    public void SaveName(string value)
    {
        SaveSystem.Data.playerName = value;
        SaveSystem.Save();
    }

    public void SaveColor(int index)
    {
        SaveSystem.Data.playerColor = index;
        SaveSystem.Save();
    }

    public void SetFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;
        SaveSystem.Data.fullscreen = isOn;
        SaveSystem.Save();
    }

    public void SetMusic(bool isOn)
    {
        bgSound.mute = !isOn;
        SaveSystem.Data.music = isOn;
        SaveSystem.Save();
    }

    public void ResetAll()
    {
        SaveSystem.ResetProgress();
        Start();
    }
}
