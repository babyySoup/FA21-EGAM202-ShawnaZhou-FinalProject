using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float fireSpeed;
    float timer;

    public GameObject bulletPrefab;
    public Transform shotPoint;

    public float moveSpeed;
    Vector3 moveAmt;
    public float health;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        //look at the direction of the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundFloor = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundFloor.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            transform.LookAt(new Vector3(point.x, 1f, point.z));
        }


        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        moveAmt = moveDirection * moveSpeed * Time.deltaTime;




        //player use left click to shoot
        timer += Time.deltaTime;
        if (timer > 1/fireSpeed && Input.GetMouseButton(0))
        {
            timer = 0;
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        //move to new position
        rb.MovePosition(rb.position + moveAmt);
    }

    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = shotPoint.forward * 10f;
        Destroy(bullet, 8f);

    }

    //player taking damage
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Creature")
        {
            health -= other.gameObject.GetComponent<Creature>().Damage;
            if (health <= 0)
            {
                //player die
                GameObject.Find("GameOverText").SetActive(true);
            }

        }
    }
}
