using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeStats : MonoBehaviour
{
    public float avgSpeed;
    public float gravity;
    public float tilt;
    public float knifeDmg;
    public float minSpeedMultiplier;
    public float maxSpeedMultiplier;

    float objGravity = 0;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        avgSpeed = avgSpeed * Random.Range(minSpeedMultiplier, maxSpeedMultiplier);

    }

    // Update is called once per frame
    void Update()
    {
        objGravity += gravity * Time.deltaTime;
        direction = -transform.forward;
        direction.y = objGravity;

        transform.Translate(direction * avgSpeed * Time.deltaTime, Space.World);
        transform.RotateAround(transform.position, transform.right, tilt * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Creature")
        {
            //destroy knife
            Destroy(this.gameObject);

            //hit enemy and damage them
            Creature creature = other.gameObject.GetComponent<Creature>();
            creature.health -= 5;
        }
    }
}
