using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public enum State { Patrol, Chase, WindUp, Attack, Cooldown }
    public State state = State.Patrol;

    public Transform[] points;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 1.8f;
    public float loseDistance = 14f;
    public float windUpTime = 0.5f;
    public float cooldownTime = 1.2f;
    public int damage = 1;
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

        rend.material.color = normalColor;
        GoToNextPoint();
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case State.Patrol:
                agent.speed = patrolSpeed;
                if (!agent.pathPending && agent.remainingDistance < 0.5f) GoToNextPoint();
                if (vision.CanSeePlayer()) state = State.Chase;
                break;

            case State.Chase:
                agent.speed = chaseSpeed;
                agent.SetDestination(player.position);
                if (dist < attackRange)
                {
                    agent.ResetPath();
                    rend.material.color = windUpColor;
                    timer = windUpTime;
                    state = State.WindUp;
                }
                else if (dist > loseDistance)
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
                if (dist < attackRange + 0.5f) player.GetComponent<Health>().TakeDamage(damage);
                rend.material.color = normalColor;
                timer = cooldownTime;
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
