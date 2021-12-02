using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject Creature1;

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
                //random generate new enemies 
                Instantiate(Creature1, new Vector3(Random.Range(-27, 27), 0.5f, Random.Range(-27, 27)), Quaternion.identity);
            }
            
        }
    }
}
