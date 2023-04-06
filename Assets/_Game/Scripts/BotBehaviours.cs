using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BotBehaviours : Character
{
    [SerializeField] public GameObject[] brickSpawns;
    [SerializeField] public Transform target;
    [SerializeField] public int maxBrickHolder = 20;
    [SerializeField] public bool isPickBrick;

    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
    }

    public override void Start()
    {
        ChangeState(new PickupBrick());
    }

    public GameObject GetClosestBrick(GameObject[] bricks)
    {
        GameObject closestBrick = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject brick in bricks)
        {
            Brick brickComponent = brick.GetComponent<Brick>();
            if (brickComponent)
            {
                float distance = Vector3.Distance(currentPosition, brick.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBrick = brick;
                }
            }
        }
        return closestBrick;
    }

    public void FindSameColorBrickSpawns()
    {
        GameObject[] allBrickSpawns = GameObject.FindGameObjectsWithTag("Brick");
        List<GameObject> sameColorBrickSpawns = new List<GameObject>();
        foreach (GameObject brickSpawn in allBrickSpawns)
        {
            Brick brick = brickSpawn.GetComponent<Brick>();
            if (brick && brick.GetNumColor() == numColor)
            {
                sameColorBrickSpawns.Add(brickSpawn);
            }
        }
        brickSpawns = sameColorBrickSpawns.ToArray();
    }
}
