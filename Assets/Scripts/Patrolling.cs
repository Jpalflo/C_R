using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private GameObject patrolPointsContainer;
    [SerializeField]private List<Transform> patrolPoints = new List<Transform>();

    [SerializeField] private GameObject wanderPointsContainer;
    [SerializeField]private List<BoxCollider> wanderPoints = new List<BoxCollider>();



    private int destinationPoint = 0; //internal index to next destination
    private int currentPoint = 0; //internal index to next destination
    private bool isChased = false; //is Chasing Player

    [SerializeField]private NavMeshAgent agent;

    private Renderer enemyRenderer;

    //Player
    private Transform playerTransform;

    [SerializeField]private float wanderTime;
    [SerializeField] private float wanderTimeCounter = 4f;


    private void Start()
    {

        //get Components
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //weaponController = GetComponent<WeaponController>();
        enemyRenderer = GetComponentInChildren<Renderer>();

        


        //Take all the children of patrolPointContainer and add them in the patrolPoints array
        foreach (Transform child in patrolPointsContainer.transform)
            patrolPoints.Add(child);

        foreach (Transform child in wanderPointsContainer.transform)
        {
            wanderPoints.Add(child.GetComponent<BoxCollider>());
        }

        //Randomly choose first destination point
        destinationPoint = Random.Range(0, patrolPoints.Count);
        currentPoint = destinationPoint;


        //First time go to Next Patrol
        FirstPatrol();

    }


    private void Update()
    {

        if (!agent.hasPath)
        {
            wanderTime -= Time.deltaTime;

        }

        //search player with Ray Cast
        SearchPlayer();
        if (isChased == false)
        {

            if (wanderTime <= 0) /// ponerlo con un timer
            {
                Wander();
            }
        }


    }

    /// <summary>
    /// first destination
    /// </summary>
    private void FirstPatrol()
    {
        //set the agent to the currently destination Point
        agent.isStopped = false;
        agent.SetDestination(patrolPoints[destinationPoint].position);
    }

    /// <summary>
    /// change destination randomly
    /// </summary>
    private void ChangeDestination()
    {
        if (!isChased) ///esto
        {    isChased = true;/// y esto para que la condicion se cumpla una vez
        

            destinationPoint = Random.Range(0, patrolPoints.Count);
            if (destinationPoint == currentPoint)//se iguala por si sale el mismo numero y cambiarlo
            {
                destinationPoint = Random.Range(0, patrolPoints.Count);

            }
            currentPoint = destinationPoint;
            agent.SetDestination(patrolPoints[destinationPoint].position);
            agent.isStopped = false;
        }
        
    }


    /// <summary>
    /// Enemy search and go other direction to destination point
    /// </summary>
    private void SearchPlayer()
    {
        NavMeshHit hit;
        //if no obstacles between enemy and player
        if (!agent.Raycast(playerTransform.position, out hit))
        {
            //Go towards Player only if is at 10m or lower
            if (hit.distance <= 10f)
            {
                ChangeDestination();
                transform.LookAt(agent.transform);
                agent.speed = 8f;
            }
            ////If the player more than 10f distance
            else
            {
                agent.isStopped = false;
                isChased = false;
            }
        }
        //Player Not in the Ray Cast
        else
        {
            agent.isStopped = false;
            isChased = false;
        }

    }

    /// <summary>
    /// Handle when the enemy receive a bullet
    /// </summary>
    /// <param name="quantity">Damage quantity</param>
    public void DamageEnemy(int quantity)
    {
        //currentLife -= quantity;
        //if (currentLife <= 0)
        //    Destroy(gameObject);
    }

    /// <summary>
    /// void to wander randomly inside a box bounds
    /// </summary>
    private void Wander()
    {
        wanderTime = wanderTimeCounter;
        currentPoint = destinationPoint;
        Vector3 wanderMove = new Vector3(Random.Range(wanderPoints[currentPoint].bounds.min.x, wanderPoints[currentPoint].bounds.max.x), Random.Range(wanderPoints[currentPoint].bounds.min.y, wanderPoints[currentPoint].bounds.max.y), Random.Range(wanderPoints[currentPoint].bounds.min.z, wanderPoints[currentPoint].bounds.max.z));
        agent.SetDestination(wanderMove);
    }
}
