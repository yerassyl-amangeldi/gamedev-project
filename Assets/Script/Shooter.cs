using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float cooldown = 0.3f;
    public AudioClip shootClip;

    private float nextShot;
    private Vector3 aimDir = Vector3.forward;
    private AudioSource src;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void SetAim(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0.001f)
            aimDir = new Vector3(direction.x, 0f, direction.y).normalized;
    }

    public void Shoot()
    {
        if (bulletPrefab == null || Time.time < nextShot) return;
        nextShot = Time.time + cooldown;

        Vector3 pos = transform.position + aimDir * 0.8f;
        Instantiate(bulletPrefab, pos, Quaternion.LookRotation(aimDir));

        if (src != null && shootClip != null) src.PlayOneShot(shootClip);
    }
}
