
using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Transform player;
    public Rock fireballPrefab;

    public float attackInterval = 3f;
    public float projectileSpeed = 8f;


    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            Shoot();

            BossHealth health = GetComponent<BossHealth>();

            float delay = attackInterval;

            if (health != null && health.CurrentHp == 5)
            {
                delay = 0.2f;
            }

            yield return new WaitForSeconds(delay);
        }
    }



    
void Shoot()
{
    if (player == null || fireballPrefab == null)
        return;

    Vector3 direction = player.position - transform.position;
    direction.y = 0;

    if (direction.sqrMagnitude < 0.01f)
        return;

    direction.Normalize();

    BossHealth hp = GetComponent<BossHealth>();

    if (hp != null && hp.CurrentHp == 25)
    {
        ShootFireball(direction, -20, -0.85f);
        ShootFireball(direction, 0, 0);
        ShootFireball(direction, 20, 0.85f);
    }
    else
    {
        ShootFireball(direction, 0, 0);
    }
}

void ShootFireball(Vector3 direction, float angle, float side)
{
    Vector3 shotDirection = Quaternion.Euler(0, angle, 0) * direction;
    Vector3 right = Vector3.Cross(Vector3.up, direction);

    Vector3 position = transform.position + direction * 2.5f + right * side;
    position.y = player.position.y + 0.5f;

    Rock ball = Instantiate(
        fireballPrefab,
        position,
        Quaternion.LookRotation(shotDirection)
    );

    Rigidbody rb = ball.GetComponent<Rigidbody>();
    rb.useGravity = false;
    rb.linearVelocity = shotDirection * projectileSpeed;
}


}
