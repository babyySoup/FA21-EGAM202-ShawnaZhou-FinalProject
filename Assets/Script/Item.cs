using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Item[] Inventory;
    public int currentItemIndex;

    public virtual void Use()
    {
        Debug.Log("You use your item! no work");
    }
    public virtual void Wear()
    {
        Debug.Log("You attempt to wear the item, wont do!");
    }
    public virtual void Eat()
    {
        Debug.Log("You try to eat item! it does not wanna be eaten tho");
    }
}
