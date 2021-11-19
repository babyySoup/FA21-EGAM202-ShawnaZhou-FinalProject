using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : Item
{
    public override void Eat()
    {
        //cabbage make avatar smaller
        transform.parent.transform.localScale *= 0.5f;
        Debug.Log("You eat the magic cabbage, but it shrinked you!");

        transform.parent.GetComponent<AvatarController>().Inventory[currentItemIndex] = null;
        Destroy(this.gameObject);
    }
}
