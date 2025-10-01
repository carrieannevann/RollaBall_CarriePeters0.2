// Assets/Scripts/EnemyChaseDebug.cs
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChaseDebug : MonoBehaviour
{
    [Header("Target")]
    public Transform player;                 // assign in Inspector or tag the Player object "Player"

    [Header("Debugging")]
    public float debugLogInterval = 0.5f;    // how often to print debug info
    public float sampleRadius = 2f;          // how far to search for nearest NavMesh if off it

    [Header("Tuning")]
    public bool disableAvoidanceOnStart = false;

    private NavMeshAgent agent;
    private float logTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
agent.updateRotation = true;


        // fallback: find player by tag if not assigned in inspector
        if (player == null)
        {
            var found = GameObject.FindGameObjectWithTag("Player");
            if (found != null) player = found.transform;
        }

        // Aggressive chase tuning
        agent.stoppingDistance = 0f;
        agent.autoBraking = false;
        agent.speed = Mathf.Max(agent.speed, 3f);
        agent.acceleration = Mathf.Max(agent.acceleration, 8f);
        agent.angularSpeed = Mathf.Max(agent.angularSpeed, 120f);

        if (disableAvoidanceOnStart)
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    void Update()
    {
        if (player == null) return;

        // If agent isn't placed on the NavMesh, try to sample nearest NavMesh and warp to it
        if (!agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, sampleRadius, NavMesh.AllAreas))
            {
                Debug.LogWarning($"{name}: Not on NavMesh — warping to nearest NavMesh point {hit.position}.");
                agent.Warp(hit.position);
            }
            else
            {
                Debug.LogWarning($"{name}: Not on NavMesh and no NavMesh within {sampleRadius} m. Re-bake NavMesh or increase sampleRadius.");
            }
        }

        // Always (re-)issue destination each frame while debugging
        if (agent.isOnNavMesh)
            agent.SetDestination(player.position);

        // Periodic debug logging
        logTimer += Time.deltaTime;
        if (logTimer >= debugLogInterval)
        {
            logTimer = 0f;
            Debug.Log($"{name} debug: isOnNavMesh={agent.isOnNavMesh}, hasPath={agent.hasPath}, pathPending={agent.pathPending}, pathStatus={agent.pathStatus}, remaining={agent.remainingDistance:F2}, stoppingDist={agent.stoppingDistance}");

            if (player != null)
            {
                var pAgent = player.GetComponent<NavMeshAgent>();
                var pObstacle = player.GetComponent<NavMeshObstacle>();
                if (pAgent != null) Debug.Log($"{player.name} HAS NavMeshAgent (avoidance may be mutual).");
                if (pObstacle != null) Debug.Log($"{player.name} HAS NavMeshObstacle (carving?) carve={pObstacle.carving}.");
            }
        }
    }
}
