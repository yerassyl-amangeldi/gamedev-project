using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    public enum State { Idle, WindUp, Shoot, Cooldown }
    public State state = State.Idle;

    public EnemyStats stats;
    public Rock bulletPrefab;
    public Color normalColor = new Color(0.6f, 0.2f, 0.9f);
    public Color windUpColor = Color.yellow;

    private NavMeshAgent agent;
    private EnemyVision vision;
    private Renderer rend;
    private Transform player;
    private Vector3 home;
    private float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<EnemyVision>();
        rend = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        vision.viewDistance = stats.viewDistance;
        vision.viewAngle = stats.viewAngle;
        GetComponent<EnemyHealth>().hp = stats.hp;
        agent.speed = stats.speed;

        home = transform.position;
        rend.material.color = normalColor;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                transform.Rotate(0f, stats.turnSpeed * Time.deltaTime, 0f);
                if (vision.CanSeePlayer() && rend.isVisible)
                {
                    agent.ResetPath();
                    rend.material.color = windUpColor;
                    timer = stats.windUpTime;
                    state = State.WindUp;
                }
                break;

            case State.WindUp:
                LookAtPlayer();
                timer -= Time.deltaTime;
                if (!vision.CanSeePlayer())
                {
                    rend.material.color = normalColor;
                    state = State.Idle;
                }
                else if (timer <= 0f)
                {
                    state = State.Shoot;
                }
                break;

            case State.Shoot:
                Fire();
                rend.material.color = normalColor;
                MoveToNewSpot();
                timer = stats.cooldownTime;
                state = State.Cooldown;
                break;

            case State.Cooldown:
                timer -= Time.deltaTime;
                if (timer <= 0f) state = State.Idle;
                break;
        }
    }

    private void Fire()
    {
        if (bulletPrefab == null) return;

        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        dir.Normalize();

        Vector3 pos = transform.position + dir * 1.2f;
        pos.y = player.position.y + 0.5f;

        Rock ball = Instantiate(bulletPrefab, pos, Quaternion.LookRotation(dir));
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = dir * stats.fastSpeed;
    }

    private void MoveToNewSpot()
    {
        Vector3 random = home + new Vector3(Random.Range(-stats.range, stats.range), 0f, Random.Range(-stats.range, stats.range));
        agent.SetDestination(random);
    }

    private void LookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f) transform.rotation = Quaternion.LookRotation(dir);
    }
}
