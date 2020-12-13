using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Char : MonoBehaviour
{
    //Every type of character should have MoveAI - but not every type should have access to all states
    public moveAI ai;
    public AIBehaviour behaviour;
    public FlowField flowfield;

    

    public int health { get; protected set; }
    protected abstract int defaultHealth { get; }

    private void Awake() {
        health = defaultHealth;
        ai = gameObject.GetComponent<moveAI>();
        behaviour = gameObject.GetComponent<AIBehaviour>();
    }


    public void damage(int dmg) {

        health = health - dmg;
    
    }

    public void heal(int heal) {

        health = health + heal;
    
    }



}
