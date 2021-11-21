using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Survival : MonoBehaviour
{
    //HP system  
    //fix thirst hunger later
    public float maxHP, maxThirst, maxHunger;
    public float health, thirst, hunger;
    public float thirstRate, hungerRate;

    public Healthbar healthbar;

    //other survival tracker
    public float damage;

    public static bool triggeringAI;
    public static GameObject triggerAI;




    public void Start()
    {
        health = maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        if (thirst < maxThirst)
        {
            thirst += thirstRate * Time.deltaTime;
        }

        if (hunger < maxHunger)
        {
            hunger += hungerRate * Time.deltaTime;
        }
        
        if (thirst >= maxThirst)
        {
            Death();
        }
        if (hunger >= maxHunger)
        {
            Death();
        }


        if (triggeringAI && triggerAI)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack(triggerAI);
            }
        }

    }

    //attack creature
    public void Attack(GameObject target)
    {
        if (target.tag == "Creature")
        {
            //Animal creature = target.GetComponent("CreatureS");
            //creature.health -= damage;

        }
    }








    public void Death()
    {
        print("you have dead in the Dark City");
    }

    public void TakeHit(int damage)
    {
        health -= damage;
        //healthbar.fillAmount((float)health / (float)maxHP);


    }

    //creature collision with the player
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Creature")
        {
            triggerAI = other.gameObject;
            triggeringAI = true; 
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Creature")
        {
            triggeringAI = false;
            triggerAI = null;
        }
    }

}
