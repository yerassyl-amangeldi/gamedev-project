using UnityEngine;

public class BombHazard : Hazard
{
    public GameObject explosionPrefab;      
    bool armed; 


    protected override void OnHitPlayer(Health p)
    {
        p.TakeDamage(items.bombHitDamage);
    }

    protected override void OnLanded()
    {
        if (armed) return;
        armed = true;
        Invoke(nameof(Explode), items.bombFuse);       
    }

    void Explode()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, items.bombRadius))
        {
            Health h = col.GetComponent<Health>();
            if (h != null) h.TakeDamage(items.bombExplosionDamage);
        }

        ShowExplosion();
        Destroy(gameObject);
    }

    void ShowExplosion()
    {
        GameObject fx;
        if (explosionPrefab != null)
        {
            fx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            fx = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            fx.transform.position = transform.position;
            fx.transform.localScale = Vector3.one * items.bombRadius * 2f;
            Destroy(fx.GetComponent<Collider>());
            fx.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0.1f);
        }
        Destroy(fx, 0.4f);
    }
}
