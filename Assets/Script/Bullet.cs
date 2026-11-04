using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 3f;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;   

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;                      
        rb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifetime);              
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || other.CompareTag("Player")) return;

        Hazard h = other.GetComponent<Hazard>();
        if (h != null)
        {
            if (!h.Shootable) return;     
            Destroy(h.gameObject);        
        }

        Destroy(gameObject);             
    }
}
