using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBrickState : IState<Character>
{
    public void OnEnter(Character character)
    {
    }

    public void OnExecute(Character character)
    {
        BotBehaviours bot = (BotBehaviours)character;
        
    }

    public void OnExit(Character character)
    {
    }

}
