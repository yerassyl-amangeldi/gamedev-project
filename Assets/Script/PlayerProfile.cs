using UnityEngine;
using TMPro;

public class PlayerProfile : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    public Color[] colors = { Color.red, Color.blue, Color.green };

    void Start()
    {
        nameText.text = SaveSystem.Data.playerName;

        int colorIndex = Mathf.Clamp(SaveSystem.Data.playerColor, 0, colors.Length - 1);
        GetComponentInChildren<Renderer>().material.color = colors[colorIndex];
    }
}
