using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class NPController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float runningSpeed = 8f;
    [SerializeField] private float dangerDelay = 3f;
    [SerializeField] private float randomPointSearchRadius = 5f;

    [SerializeField] private Animator anim;
    enum STATE { IDLE,WANDER, FOLLOW, RUNAWAY}
    STATE state = STATE.IDLE;
    public string stateName;
    private Vector3 target;
    private Vector3 dangerPosition;
    private float timer = 0f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void TurnOffTriggers()
    {
        //turning off triggers in animation
        anim.SetBool("walking", false);
        anim.SetBool("running", false);
    }
    private void Update()
    {
        //state machine
        switch (state)
        {
            case STATE.IDLE:
                //randomly every tick could go to wander state
                if (Random.Range(0, 5000) < 5)
                    state = STATE.WANDER;
                stateName = "idle";

                break;
            case STATE.WANDER:
                if (!agent.hasPath)
                {
                    // finding destination to move there
                    target = Vector3.zero;
                    while (target == Vector3.zero)
                    {
                        target = RandomNavmeshLocation(randomPointSearchRadius);
                    }
                    agent.SetDestination(target);
                    agent.stoppingDistance = 0.0075f;
                    TurnOffTriggers();
                    agent.speed = walkingSpeed;
                    anim.SetBool("walking", true);
                    Debug.Log(agent.remainingDistance);
                }
                // achieved destination and goes back to idle state
                if (agent.remainingDistance <= agent.stoppingDistance + 0.1f && !agent.pathPending)
                {
                    Debug.Log("Stopped");
                    TurnOffTriggers();
                    state = STATE.IDLE;
                    agent.ResetPath();
                }

                stateName = "wander";
                break;
            case STATE.RUNAWAY:
                Debug.Log("Runaway");
                if (!agent.hasPath)
                {
                    // making a vector that goes away from danger point
                    target = Vector3.zero;
                    while (target == Vector3.zero)
                    {
                        target = transform.position - dangerPosition;
                        Debug.Log(target);
                    }
                    agent.SetDestination(transform.position + target.normalized * 5);
                    TurnOffTriggers();
                    agent.speed = runningSpeed;
                    anim.SetBool("running", true);
                }
                {
                    // After some time returns back to idle state
                    timer += Time.deltaTime;
                    if( timer > dangerDelay)
                    {
                        timer = 0;
                        TurnOffTriggers();
                        state = STATE.IDLE;
                        agent.ResetPath();
                    }
                }

                stateName = "run away";
                break;
        }
    }
    
    public void DangerTrigger(Collider other)
    {
        //called from hitbox to transfer dangerposition
        dangerPosition = other.transform.position;
        TurnOffTriggers();
        state = STATE.RUNAWAY;
        agent.ResetPath();
    }
    public Vector3 RandomNavmeshLocation(float radius)
    {
        // Seeks for random point at some radius that also placed at navmesh
        Vector3 randomDirection = Vector3.zero;
        while (randomDirection.magnitude < radius * 0.85)
        {
             randomDirection = Random.insideUnitSphere * radius;
        }
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
