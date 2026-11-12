using UnityEngine;
using UnityEngine.AI;

public class EnemyCharger : MonoBehaviour
{
    public enum State { Walk, WindUp, Charge, Tired }
    public State state = State.Walk;

    public float walkSpeed = 1.5f;
    public float noticeDistance = 9f;
    public float windUpTime = 0.7f;
    public float chargeSpeed = 14f;
    public float chargeTime = 0.6f;
    public float tiredTime = 1.5f;
    public int damage = 1;
    public Color normalColor = new Color(1f, 0.5f, 0f);
    public Color windUpColor = Color.yellow;
    public Color tiredColor = Color.gray;

    private NavMeshAgent agent;
    private EnemyVision vision;
    private Renderer rend;
    private Transform player;
    private Vector3 chargeDir;
    private float timer;
    private bool hitDone;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<EnemyVision>();
        rend = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rend.material.color = normalColor;
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case State.Walk:
                agent.speed = walkSpeed;
                agent.SetDestination(player.position);
                if (dist < noticeDistance && vision.CanSeePlayer())
                {
                    agent.ResetPath();
                    rend.material.color = windUpColor;
                    timer = windUpTime;
                    state = State.WindUp;
                }
                break;

            case State.WindUp:
                LookAtPlayer();
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    chargeDir = transform.forward;
                    rend.material.color = normalColor;
                    hitDone = false;
                    timer = chargeTime;
                    state = State.Charge;
                }
                break;

            case State.Charge:
                agent.Move(chargeDir * chargeSpeed * Time.deltaTime);
                if (!hitDone && dist < 1.5f)
                {
                    player.GetComponent<Health>().TakeDamage(damage);
                    hitDone = true;
                }
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    rend.material.color = tiredColor;
                    timer = tiredTime;
                    state = State.Tired;
                }
                break;

            case State.Tired:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    rend.material.color = normalColor;
                    state = State.Walk;
                }
                break;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.01f) transform.rotation = Quaternion.LookRotation(dir);
    }
}
