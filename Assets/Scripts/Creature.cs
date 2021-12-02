using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    public float moveSd = 5f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(Vector3.zero);
        rb.velocity = transform.forward * moveSd;
    }

    private void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
