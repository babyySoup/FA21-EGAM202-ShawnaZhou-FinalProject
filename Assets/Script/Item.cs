using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Item[] Inventory;
    public int currentItemIndex;

    public virtual void Attack()
    {
        Debug.Log("You attack");
    }
    public virtual void Talk()
    {
        Debug.Log("You start a conversation");
    }
    public virtual void Use()
    {
        Debug.Log("You use item");
    }
    public virtual void Eat()
    {
        Debug.Log("You eat item");
    }

}
