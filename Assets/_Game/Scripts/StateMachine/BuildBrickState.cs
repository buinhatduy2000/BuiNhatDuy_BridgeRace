using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBrickState : IState<Character>
{
    public void OnEnter(Character character)
    {
    }

    public void OnExecute(Character character)
    {
        BotBehaviours bot = (BotBehaviours)character;

        bot.navMeshAgent.SetDestination(bot.target.position);
    }

    public void OnExit(Character character)
    {

    }

}
