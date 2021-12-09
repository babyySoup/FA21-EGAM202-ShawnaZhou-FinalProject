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
}
