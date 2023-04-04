using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BotBehaviours : Character
{
    [SerializeField] public GameObject[] brickSpawns;
    [SerializeField] public Transform target;
    [SerializeField] public int maxBrickHolder = 10;

    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FindSameColorBrickSpawns();
        ChangeAnimation("Run");

        if (brickHolder.childCount == 0)
        {
            MoveToClosestBrickOfType(brickSpawns, numColor);
        }
        else if (brickHolder.childCount >= maxBrickHolder)
        {
            MoveToTarget();
            Debug.Log("Move to target");
        }
        else
        {
            MoveToClosestBrickOfType(brickSpawns, numColor);
        }
    }

    public void MoveToTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    public GameObject GetClosestBrickOfType(GameObject[] bricks, int color)
    {
        GameObject closestBrick = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject brick in bricks)
        {
            Brick brickComponent = brick.GetComponent<Brick>();
            if (brickComponent && brickComponent.GetNumColor() == color)
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

    public void MoveToClosestBrickOfType(GameObject[] bricks, int color)
    {
        GameObject closestBrick = GetClosestBrickOfType(bricks, color);
        if (closestBrick != null)
        {
            navMeshAgent.SetDestination(closestBrick.transform.position);
        }
    }

    private void FindSameColorBrickSpawns()
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
