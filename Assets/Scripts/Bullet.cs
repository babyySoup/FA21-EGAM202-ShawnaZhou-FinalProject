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
            creature.health -= 20;

            if(creature.health <= 0)
            {
                Destroy(other.gameObject);
                //score player
                PlayerScore ps = GameObject.Find("Player").GetComponent<PlayerScore>();
                ps.KillCount++;
                print(ps.KillCount);

                int SpawnCreature = Random.Range(0, 2);
                //random generate new enemies 
                if(SpawnCreature == 0)
                {
                    Instantiate(Creature1, new Vector3(Random.Range(-27, 27), 0.5f, Random.Range(-27, 27)), Quaternion.identity);
                }else if(SpawnCreature == 1)
                {
                    Instantiate(Creature2, new Vector3(Random.Range(-27, 27), 0.5f, Random.Range(-27, 27)), Quaternion.identity);
                }
                
   
            }
            
        }
    }
}
