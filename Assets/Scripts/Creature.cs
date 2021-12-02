using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{

    public float moveSd = 5f;
    public float health;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        rb = GetComponent<Rigidbody>();
        //transform.LookAt(Vector3.zero);
        //rb.velocity = transform.forward * moveSd;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }

    void Update()
    {

    }
}
