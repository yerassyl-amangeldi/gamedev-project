using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int hp;
    public int damage;
    public float speed;
    public float fastSpeed;
    public float turnSpeed;
    public float viewDistance;
    public float viewAngle;
    public float range;
    public float loseDistance;
    public float windUpTime;
    public float attackTime;
    public float cooldownTime;
}
