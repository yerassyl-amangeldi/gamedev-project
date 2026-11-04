using UnityEngine;
using UnityEngine.Rendering;

public class LevelStyler : MonoBehaviour
{
    [Header("Цвета (можно менять в Inspector)")]
    public Color backgroundColor = new Color(0.05f, 0.07f, 0.14f);
    public Color ambientColor    = new Color(0.35f, 0.40f, 0.55f);
    public Color floorColor      = new Color(0.16f, 0.20f, 0.30f);
    public Color wallColor       = new Color(0.10f, 0.12f, 0.20f);
    public Color platformColor   = new Color(0.25f, 0.35f, 0.85f);
    public Color padColor        = new Color(0.90f, 0.30f, 1.00f);
    public Color coinColor       = new Color(1.00f, 0.78f, 0.10f);
    public Color enemyColor      = new Color(1.00f, 0.15f, 0.15f);
    public Color exitColor       = new Color(0.30f, 0.95f, 1.00f);

    [Header("Настройки")]
    public float fogDensity = 0.012f;   
    public bool animateCoins = true;  

    private GameObject[] coins;
    private float[] baseY;

    void Start()
    {
        SetupCameraAndFog();
        SetupLight();

        GameObject floor = GameObject.Find("Plane");
        if (floor != null) Paint(floor, floorColor, 0f);

        foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
        {
            string n = r.gameObject.name;
            if (n.StartsWith("Wall")) Paint(r.gameObject, wallColor, 0f);
            else if (n.StartsWith("Platform")) Paint(r.gameObject, platformColor, 0f);
            else if (n.StartsWith("BouncePad")) Paint(r.gameObject, padColor, 0.8f);
        }

        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
            Paint(e, enemyColor, 1.5f);
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Exit"))
            Paint(x, exitColor, 1.5f);

        coins = GameObject.FindGameObjectsWithTag("Coin");
        baseY = new float[coins.Length];
        for (int i = 0; i < coins.Length; i++)
        {
            Paint(coins[i], coinColor, 1.2f);
            baseY[i] = coins[i].transform.position.y;
        }
    }

    void Update()
    {
        if (!animateCoins || coins == null) return;

        for (int i = 0; i < coins.Length; i++)
        {
            if (coins[i] == null) continue; 
            Vector3 p = coins[i].transform.position;
            p.y = baseY[i] + Mathf.Sin(Time.time * 2f + i) * 0.15f;
            coins[i].transform.position = p;
        }
    }

    void SetupCameraAndFog()
    {
        Camera cam = Camera.main;
        if (cam == null) cam = FindFirstObjectByType<Camera>();
        if (cam != null)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = backgroundColor;
        }

        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = ambientColor;

        if (fogDensity > 0f)
        {
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogColor = backgroundColor;
            RenderSettings.fogDensity = fogDensity;
        }
    }

    void SetupLight()
    {
        if (FindFirstObjectByType<Light>() != null) return;

        GameObject sun = new GameObject("Sun");
        Light l = sun.AddComponent<Light>();
        l.type = LightType.Directional;
        l.color = new Color(1f, 0.95f, 0.85f);
        l.intensity = 1.3f;
        l.shadows = LightShadows.Soft;
        sun.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
    }

    void Paint(GameObject go, Color color, float glow)
    {
        Renderer r = go.GetComponent<Renderer>();
        if (r == null) return;

        Material m = r.material; 
        m.color = color;

        if (glow > 0f)
        {
            m.EnableKeyword("_EMISSION");
            m.SetColor("_EmissionColor", color * glow);
        }
    }
}