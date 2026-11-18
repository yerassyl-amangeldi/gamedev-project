using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hazard : MonoBehaviour
{
    public float lifetime = 12f;
    public ItemStats items;

    public virtual bool Shootable => true;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            OnHitPlayer(c.gameObject.GetComponent<Health>());
            Destroy(gameObject);
        }
        else
        {
            OnLanded();   
        }
    }

    protected virtual void OnHitPlayer(Health player)
    {
        player.TakeDamage(items.rockDamage);
    }

    protected virtual void OnLanded() { }
}
