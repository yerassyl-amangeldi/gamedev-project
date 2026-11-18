using UnityEngine;

[CreateAssetMenu(menuName = "Game/Boss Stats")]
public class BossStats : ScriptableObject
{
    public int maxHp;
    public float firstDelay;
    public float attackInterval;
    public float projectileSpeed;
    public int rageHp;
    public float rageInterval;
}
