using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public enum State { Patrol, Chase, WindUp, Attack, Cooldown }
    public State state = State.Patrol;

    public EnemyStats stats;
    public Transform[] points;
    public Color normalColor = new Color(0.2f, 0.4f, 1f);
    public Color windUpColor = Color.yellow;

    private NavMeshAgent agent;
    private EnemyVision vision;
    private Renderer rend;
    private Transform player;
    private int pointIndex;
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

        rend.material.color = normalColor;
        GoToNextPoint();
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case State.Patrol:
                agent.speed = stats.speed;
                if (!agent.pathPending && agent.remainingDistance < 0.5f) GoToNextPoint();
                if (vision.CanSeePlayer()) state = State.Chase;
                break;

            case State.Chase:
                agent.speed = stats.fastSpeed;
                agent.SetDestination(player.position);
                if (dist < stats.range)
                {
                    agent.ResetPath();
                    rend.material.color = windUpColor;
                    timer = stats.windUpTime;
                    state = State.WindUp;
                }
                else if (dist > stats.loseDistance)
                {
                    GoToNextPoint();
                    state = State.Patrol;
                }
                break;

            case State.WindUp:
                LookAtPlayer();
                timer -= Time.deltaTime;
                if (timer <= 0f) state = State.Attack;
                break;

            case State.Attack:
                if (dist < stats.range) player.GetComponent<Health>().TakeDamage(stats.damage);
                rend.material.color = normalColor;
                timer = stats.cooldownTime;
                state = State.Cooldown;
                break;

            case State.Cooldown:
                timer -= Time.deltaTime;
                if (timer <= 0f) state = State.Chase;
                break;
        }
    }

    private void GoToNextPoint()
    {
        if (points.Length == 0) return;
        agent.SetDestination(points[pointIndex].position);
        pointIndex = (pointIndex + 1) % points.Length;
    }

    private void LookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f) transform.rotation = Quaternion.LookRotation(dir);
    }
}
