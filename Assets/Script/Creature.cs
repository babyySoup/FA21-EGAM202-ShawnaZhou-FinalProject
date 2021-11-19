using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{

    public float health;
    public int NumOfItems;
    public GameObject[] item;

    private UnityEngine.AI.NavMeshAgent agent;

    private bool dead;







    // Start is called before the first frame update
    void OnEnable()
    {
        item = new GameObject[NumOfItems];

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public void DropItems()
    {
        for (int i = 0; i < NumOfItems; i++)
        {
            GameObject droppedItem = Instantiate(item[i], transform.position, Quaternion.identity);
            break;
        }
    }

    public void Death()
    {
        DropItems();
        Destroy(this.gameObject);
    }
}
