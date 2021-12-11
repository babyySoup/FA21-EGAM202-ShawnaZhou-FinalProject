using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Item
{
    public override void Use()
    {
        transform.parent.GetComponent<PlayerController>().health += 20f;
        Debug.Log("You used medkit, it heals you");

        transform.parent.GetComponent<PlayerController>().Inventory[currentItemIndex] = null;
        Destroy(this.gameObject);
    }
}