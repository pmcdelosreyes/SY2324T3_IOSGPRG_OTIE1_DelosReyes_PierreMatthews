using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public Health enemyHealth;
    public Ammo enemyAmmo;
    Rigidbody2D rb2dEnemy;

    [SerializeField] State curState;

    // Waypoint AI [Patrol]
    //[SerializeField] Transform waypointParent;
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    int waypointIndex;
    [SerializeField] protected Unit detectedUnit;

    enum State
    {
        Patrol,
        Chase,
        Attack,
    }

    private void Awake()
    {
        // Initialize local variables
        enemyHealth = GetComponent<Health>();
        enemyAmmo = GetComponent<Ammo>();
        rb2dEnemy = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        waypoints = WaypointParent.Instance.GetWayPoints();
        waypointIndex = Random.Range(0, waypoints.Count); // set random starting location

        // initialize player stats
        enemyHealth.InitHealth(10);
        enemyAmmo.InitAmmo(10000, 10000, 10000); // give absurd amount of ammo
        InitUnit(enemyHealth, 2.5f, enemyAmmo);

        // set random gun
        this.curGun = SetRandGun();
    }

    Gun SetRandGun()
    {
        // assign random gun to enemy
        int randNum = Random.Range(0, 3);
        switch (randNum)
        {
            case 0:
                curGun = GetComponentInChildren<Pistol>();
                break;
            case 1:
                curGun = GetComponentInChildren<Shotgun>();

                break;
            case 2:
                curGun = GetComponentInChildren<ARifle>();

                break;
        }
        curGun.GetComponent<SpriteRenderer>().enabled = true;
        return curGun;
    }

    private void Update()
    {
        switch (curState)
        {
            case State.Patrol:
                PatrolUpdate();
                break;

            case State.Chase:
                ChaseUpdate();
                break;

            case State.Attack:
                AttackUpdate();
                break;
        }
        //Debug.Log(curState);
    }

    void PatrolUpdate()
    {
        //Debug.Log("Patrolling");
        Vector3 toPos = waypoints[waypointIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, toPos, Time.deltaTime * moveSpeed); // move towards a node

        // stay at node and wait for (3) seconds
        //Invoke(nameof(PatrolMove), 3f);

        Vector3 lookPos = toPos - transform.position; // adjust enemy rotation to direction it is going
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookPos);

        // move to next node
        if (transform.position.x == toPos.x && transform.position.y == toPos.y)
        {
            waypointIndex = Random.Range(0, waypoints.Count);
            waypointIndex = (int)Mathf.Repeat(waypointIndex, waypoints.Count);
        }

        // if other unit detected (curState = State.Chase)
        if (detectedUnit != null && Vector2.Distance(transform.position, detectedUnit.transform.position) > 4f && Vector2.Distance(transform.position, detectedUnit.transform.position) < 6f)
            curState = State.Chase;
    }

    void ChaseUpdate()
    {
        //Debug.Log("Chasing");
        // if other unit is != null, go towards enemy
        Vector3 toPos = detectedUnit.gameObject.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, toPos, Time.deltaTime * moveSpeed); // move towards a node

        Vector3 lookPos = toPos - transform.position; // adjust enemy rotation to direction it is going
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookPos);

        // else if distance is > 10, (curState = State.Patrol)
        if(detectedUnit != null && Vector2.Distance(transform.position, detectedUnit.transform.position) > 6f)
            curState = State.Patrol;

        // if distance to unit is < 5, start shooting
        if (detectedUnit != null && Vector2.Distance(transform.position, detectedUnit.transform.position) < 4f)
            curState = State.Attack;
    }

    void AttackUpdate()
    {
        //Debug.Log("Attacking");
        curGun.Shoot();

        // else if distance is < 10 & > 5, (curState = State.Chase)
        if (detectedUnit != null && Vector2.Distance(transform.position, detectedUnit.transform.position) > 4f && Vector2.Distance(transform.position, detectedUnit.transform.position) < 6f)
            curState = State.Chase;
    }

    public void SetTarget(Unit target)
    {
        detectedUnit = target;
    }
}
