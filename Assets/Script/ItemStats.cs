using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Stats")]
public class ItemStats : ScriptableObject
{
    public int rockDamage;
    public int heartHeal;
    public int bombHitDamage;
    public int bombExplosionDamage;
    public float bombFuse;
    public float bombRadius;
}
