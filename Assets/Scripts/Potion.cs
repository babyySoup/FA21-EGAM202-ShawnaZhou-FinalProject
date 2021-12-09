using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    public override void Use()
    {
        transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>().speed *= 1.5f;
        Debug.Log("You drink the potion! i think it makes you faster!");

        transform.parent.GetComponent<PlayerController>().Inventory[currentItemIndex] = null;
        Destroy(this.gameObject);
    }
}
