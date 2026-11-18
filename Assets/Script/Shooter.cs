using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public AudioClip shootClip;

    private float nextShot;
    private Vector3 aimDir = Vector3.forward;
    private AudioSource src;
    private PlayerStats stats;

    private void Start()
    {
        src = GetComponent<AudioSource>();
        stats = GetComponent<Movement>().stats;
    }

    public void SetAim(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0.001f)
            aimDir = new Vector3(direction.x, 0f, direction.y).normalized;
    }

    public void Shoot()
    {
        if (bulletPrefab == null || Time.time < nextShot) return;
        nextShot = Time.time + stats.shootCooldown;

        Vector3 pos = transform.position + aimDir * 0.8f;
        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.LookRotation(aimDir));
        bullet.GetComponent<Bullet>().speed = stats.bulletSpeed;

        if (src != null && shootClip != null) src.PlayOneShot(shootClip);
    }
}
