using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public LevelData level;
    public Spawner spawner;
    public SurvivalTimer timer;

    private void Awake()
    {
        spawner.firstDelay = level.firstDelay;
        spawner.interval = level.spawnInterval;
        if (timer != null) timer.goal = level.goalTime;
    }
}
