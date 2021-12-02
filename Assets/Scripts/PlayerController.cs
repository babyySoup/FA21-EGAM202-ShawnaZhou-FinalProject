using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float fireSpeed;
    float timer;

    public GameObject bulletPrefab;
    public Transform shotPoint;


    void Update()

        //look at the direction of the mouse
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundFloor = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundFloor.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            transform.LookAt(new Vector3(point.x, 1f, point.z));
        }

        //player use left click to shoot
        timer += Time.deltaTime;
        if (timer > 1/fireSpeed && Input.GetMouseButton(0))
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = shotPoint.forward * 10f;
        Destroy(bullet, 15f);

    }
}
