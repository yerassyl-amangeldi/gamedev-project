using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public Transform player;
    public Rock fireballPrefab;

    private BossHealth health;
    private BossStats stats;

    void Start()
    {
        health = GetComponent<BossHealth>();
        stats = health.stats;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(stats.firstDelay);

        while (true)
        {
            Shoot();

            float delay = stats.attackInterval;

            if (health.CurrentHp == stats.rageHp)
            {
                delay = stats.rageInterval;
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

        if (health.CurrentHp == stats.maxHp)
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

        Rock ball = Instantiate(fireballPrefab, position, Quaternion.LookRotation(shotDirection));

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = shotDirection * stats.projectileSpeed;
    }
}
