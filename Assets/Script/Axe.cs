using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Item
{

    public override void Use()
    {
        GetComponent<Animator>().SetTrigger("Hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Creature"))
            Destroy(other.gameObject);
    }
}