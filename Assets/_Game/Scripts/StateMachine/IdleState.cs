using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Character>
{
    public void OnEnter(Character character)
    {
    }

    public void OnExecute(Character character)
    {

    }

    public void OnExit(Character character)
    {
    }

}

public class PickupBrick : IState<Character>
{
    public void OnEnter(Character character)
    {
        BotBehaviours bot = character as BotBehaviours;
        bot.isPickBrick = true;
    }

    public void OnExecute(Character character)
    {
        BotBehaviours bot = character as BotBehaviours;
        bot.ChangeAnimation("Run");
        bot.FindSameColorBrickSpawns();

        if (bot.brickHolder.childCount < bot.maxBrickHolder)
        {
            GameObject closestBrick = bot.GetClosestBrick(bot.brickSpawns);
            if (closestBrick != null)
            {
                bot.navMeshAgent.SetDestination(closestBrick.transform.position);
            }
        }
        else
        {
            bot.ChangeState(new MoveToTarget());
        }
    }

    public void OnExit(Character character)
    {
        BotBehaviours bot = character as BotBehaviours;
        bot.isPickBrick = false;
    }
}

public class MoveToTarget : IState<Character>
{
    public void OnEnter(Character character)
    {
        BotBehaviours bot = character as BotBehaviours;
        bot.ChangeAnimation("Run");
        bot.navMeshAgent.SetDestination(bot.target.position);

    }

    public void OnExecute(Character character)
    {
        BotBehaviours bot = character as BotBehaviours;
        if (bot.brickHolder.childCount == 0)
        {
            bot.navMeshAgent.velocity = Vector3.zero;
            bot.ChangeState(new PickupBrick());
        }
    }

    public void OnExit(Character character)
    {

    }
}