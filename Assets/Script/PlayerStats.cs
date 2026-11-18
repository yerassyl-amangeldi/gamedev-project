using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float speed;
    public float jumpForce;
    public int startHp;
    public int maxHp;
    public float shootCooldown;
    public float bulletSpeed;
}
