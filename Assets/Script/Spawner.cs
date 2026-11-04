using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Hazard[] prefabs;                                
    public float firstDelay = 1f;
    public float interval = 2f;
    public Vector3 areaSize = new Vector3(16f, 0f, 16f);      

    void Start()
    {
        InvokeRepeating(nameof(Spawn), firstDelay, interval);
    }

    void Spawn()
    {
        Hazard pick = prefabs[Random.Range(0, prefabs.Length)];

        Vector3 pos = transform.position + new Vector3(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            0f,
            Random.Range(-areaSize.z / 2f, areaSize.z / 2f));

        Instantiate(pick, pos, Quaternion.identity);
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(Spawn));
    }

    
}
