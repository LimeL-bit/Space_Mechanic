using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFighterHealth))]
[RequireComponent(typeof(EnemyFighterAttack))]
public class EnemyFighterAI : MonoBehaviour
{
    public enum State
    {
        flying,
        orbit,
        enterAttack,
        attack,
        retreat,
        kamikaze,
    }

    private State currentState;

    [Header("Fighter")]
    [SerializeField] float speed = 5;
    [SerializeField] float turnSpeed = 3.5f;
    [SerializeField] float evadeOffset = 2;
    [SerializeField] float evadeTime = 1;
    [SerializeField] float attackFrequency = 4;
    [SerializeField] float ascendTime = 2;
    private float ascendTimer;
    private float nextAttackTimer;
    private bool canAttack = true;
    private float nextEvasion;
    private bool isEvadingLeft = true;
    private bool isEvading = false;

    [Header("Other")]
    [SerializeField] GameObject target;
    [SerializeField] float distance = 5;

    private Vector2 direction;
    private float angle;

    private GameObject closestWayPoint;
    private float closestWayPointDistance = 0;
    private int pathIndex = 0;


    [SerializeField] List<GameObject> orbitPath = new List<GameObject>();


    private Rigidbody2D rb;
    private EnemyFighterAttack enemyAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<EnemyFighterAttack>();

        currentState = State.flying;
        nextAttackTimer = Random.Range(0, attackFrequency + 1);
        ascendTimer = ascendTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.flying:
                FlyToTarget();
                break;
            case State.orbit:
                Orbit();
                break;
            case State.enterAttack:
                EnterAttack();
                break;
            case State.attack:
                enemyAttack.Fire(direction, angle);
                if (Vector2.Distance(target.transform.position, transform.position) < distance)
                {
                    canAttack = false;
                    currentState = State.orbit;
                }
                break;
            case State.retreat:
                Evade(transform.position - target.transform.position);
                break;
            case State.kamikaze:
                Evade(target.transform.position - transform.position);
                break;
        }


        Move();
    }

    private void FlyToTarget()
    {
        direction = target.transform.position - transform.position;

        if (Vector2.Distance(target.transform.position, transform.position) < distance)
        {
            canAttack = false;
            currentState = State.orbit;
        }
    }

    private void Orbit()
    {
        if(closestWayPoint == null)
        {
            for (int index = 0; index < orbitPath.Count; index++)
            {
                float wayPointDistance = Vector2.Distance(orbitPath[index].transform.position, transform.position);

                if (closestWayPoint != null)
                {
                    closestWayPointDistance = Vector2.Distance(closestWayPoint.transform.position, transform.position);
                }

                if (wayPointDistance < closestWayPointDistance || closestWayPoint == null)
                {
                    closestWayPoint = orbitPath[index];
                    pathIndex = index;
                }
            }
        }

        if(Vector2.Distance(closestWayPoint.transform.position, transform.position) <= 4)
        {
            pathIndex++;

            if(pathIndex < orbitPath.Count)
            {
                closestWayPoint = orbitPath[pathIndex];
            }
            else
            {
                pathIndex = 0;
                closestWayPoint = orbitPath[pathIndex];
            }
        }

            direction = closestWayPoint.transform.position - transform.position;

        if(nextAttackTimer > 0)
        {
            nextAttackTimer -= Time.deltaTime;
        }
        else
        {
            currentState = State.enterAttack;
            nextAttackTimer = Random.Range(0, attackFrequency + 1);
            direction = transform.position - target.transform.position;
        }
    }

    private void EnterAttack()
    {
        if(ascendTimer > 0)
        {
            ascendTimer -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
            ascendTimer = ascendTime;
            direction = target.transform.position - transform.position;
            currentState = State.attack;
        }
    }
    private void Evade(Vector2 direction1)
    {
        if (!isEvading)
        {
            direction = direction1;
            isEvading = true;
        }
        //zigzag evasion
        if(Time.time > nextEvasion)
        {
            nextEvasion = Time.time + evadeTime;

            if(isEvadingLeft)
            {
                direction.x = direction.x + evadeOffset;
                isEvadingLeft = false;
            }
            else
            {
                direction.x = direction.x - evadeOffset;
                isEvadingLeft = true;
            }

        }
    }

    private void Move()
    {
        float v = Vector2.SignedAngle(transform.up, direction);
        angle = Vector2.SignedAngle(Vector2.up, direction);
        rb.angularVelocity = v * turnSpeed;
        rb.linearVelocity = transform.up * speed;

    }

    public void ChangeState(State state)
    {
        currentState = state;
    }
    public void AssignTarget(GameObject target)
    {
        this.target = target;
    }

    public void AssignOrbitPath(List<GameObject> path)
    {
        orbitPath = path;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            if (canAttack)
            {
                currentState = State.attack;
            }
        }
    }
}
