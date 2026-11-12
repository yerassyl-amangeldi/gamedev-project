using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log(name + " HP: " + hp);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
