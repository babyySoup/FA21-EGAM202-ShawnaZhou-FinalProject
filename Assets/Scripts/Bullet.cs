using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject Creature1;
    public GameObject Creature2;
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Creature")
        {
            //destroy bullets
            Destroy(this.gameObject);
           
            //hit enemy and damage them
            Creature creature = other.gameObject.GetComponent<Creature>();
            creature.health -= 10;

            if(creature.health <= 0)
            {
                //kill enemy
                Destroy(other.gameObject);
                //score player
                PlayerScore ps = GameObject.Find("Player").GetComponent<PlayerScore>();
                ps.KillCount++;
                print(ps.KillCount);

            }
            
        }

        if (other.gameObject.tag == "CreatureS")
        {
            //destroy bullets
            Destroy(this.gameObject);

            //hit enemy and damage them
            CreatureS creature = other.gameObject.GetComponent<CreatureS>();
            creature.health -= 10;

            if (creature.health <= 0)
            {
                //kill enemy
                Destroy(other.gameObject);
                //score player
                PlayerScore ps = GameObject.Find("Player").GetComponent<PlayerScore>();
                ps.KillCount++;
                print(ps.KillCount);

            }

        }


        if (other.gameObject.tag == "NPC")
        {
            //destroy bullets
            Destroy(this.gameObject);
        }
    }
}
