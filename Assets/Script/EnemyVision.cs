using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float viewDistance = 10f;
    public float viewAngle = 90f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 eyes = transform.position + Vector3.up * 0.5f;
        Vector3 toPlayer = player.position - eyes;

        if (toPlayer.magnitude > viewDistance) return false;

        Vector3 flat = toPlayer;
        flat.y = 0f;
        if (Vector3.Angle(transform.forward, flat) > viewAngle / 2f) return false;

        RaycastHit hit;
        if (Physics.Raycast(eyes, toPlayer.normalized, out hit, viewDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            return hit.transform == player;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
}
