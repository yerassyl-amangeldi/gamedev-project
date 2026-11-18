using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static SaveData data;

    private static string FilePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static SaveData Data
    {
        get
        {
            if (data == null) Load();
            return data;
        }
    }

    public static void Load()
    {
        data = new SaveData();
        if (!File.Exists(FilePath)) return;

        try
        {
            SaveData loaded = JsonUtility.FromJson<SaveData>(File.ReadAllText(FilePath));
            if (loaded != null) data = loaded;
        }
        catch
        {
            Debug.LogWarning("Save file is broken, using defaults");
        }

        if (data.unlockedLevel < 1) data.unlockedLevel = 1;
    }

    public static void Save()
    {
        File.WriteAllText(FilePath, JsonUtility.ToJson(Data, true));
        Debug.Log("Saved: " + FilePath);
    }

    public static void CompleteLevel(int level, int score)
    {
        if (level + 1 > Data.unlockedLevel) Data.unlockedLevel = level + 1;
        if (score > Data.bestScore) Data.bestScore = score;
        Save();
    }

    public static void ResetProgress()
    {
        data = new SaveData();
        Save();
    }
}
