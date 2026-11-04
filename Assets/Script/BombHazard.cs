using UnityEngine;

public class BombHazard : Hazard
{
    public float fuse = 2f;
    public float explosionRadius = 3f;
    public int explosionDamage = 1;          
    public GameObject explosionPrefab;      
    bool armed; 


    protected override void OnHitPlayer(Health p)
    {
        p.TakeDamage(2);
    }

    protected override void OnLanded()
    {
        if (armed) return;
        armed = true;
        Invoke(nameof(Explode), fuse);       
    }

    void Explode()
    {
        if (explosionDamage > 0)
        {
            foreach (Collider col in Physics.OverlapSphere(transform.position, explosionRadius))
            {
                Health h = col.GetComponent<Health>();
                if (h != null) h.TakeDamage(explosionDamage);
            }
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
            fx.transform.localScale = Vector3.one * explosionRadius * 2f;
            Destroy(fx.GetComponent<Collider>());
            fx.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0.1f);
        }
        Destroy(fx, 0.4f);
    }
}
