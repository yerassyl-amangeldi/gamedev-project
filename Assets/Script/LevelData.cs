using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public float goalTime;
    public float firstDelay;
    public float spawnInterval;
}
