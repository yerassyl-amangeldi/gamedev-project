using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform panel;
    private Rect lastArea;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        Apply();
    }

    private void Update()
    {
        if (Screen.safeArea != lastArea) Apply();
    }

    private void Apply()
    {
        lastArea = Screen.safeArea;
        Vector2 min = lastArea.position;
        Vector2 max = lastArea.position + lastArea.size;
        min.x /= Screen.width;
        min.y /= Screen.height;
        max.x /= Screen.width;
        max.y /= Screen.height;
        panel.anchorMin = min;
        panel.anchorMax = max;
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;
    }
}