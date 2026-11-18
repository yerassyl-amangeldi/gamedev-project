using System;

[Serializable]
public class SaveData
{
    public int version = 1;
    public int bestScore = 0;
    public int unlockedLevel = 1;
    public string playerName = "Player";
    public int playerColor = 0;
    public bool music = true;
    public float volume = 1f;
    public bool fullscreen = true;
}
