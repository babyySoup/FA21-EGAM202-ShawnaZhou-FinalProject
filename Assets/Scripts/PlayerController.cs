using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //shooting
    public float fireSpeed;
    float timer;
    public GameObject bulletPrefab;
    public Transform shotPoint;

    //movement
    public float moveSpeed;
    Vector3 moveAmt;
    public float health;
    Rigidbody rb;

    //inventory
    public Item[] Inventory;
    public int currentItemIndex;

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

        //inventory key up arrow 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Inventory[currentItemIndex] != null)
                Inventory[currentItemIndex].gameObject.SetActive(false);

            Debug.Log("You rearrange your inventory.");
            currentItemIndex++;

            if (currentItemIndex >= Inventory.Length)
                currentItemIndex = 0;

            if (Inventory[currentItemIndex] != null)
            {
                Inventory[currentItemIndex].gameObject.SetActive(true);
                Inventory[currentItemIndex].transform.localPosition = new Vector3(0, 4, 7);
                Debug.Log("You are now holding a " + Inventory[currentItemIndex].name);

            }
            else
            {
                Debug.Log("You are not holding anything!");
            }
        }
        // down arrow
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Inventory[currentItemIndex] != null)
                Inventory[currentItemIndex].gameObject.SetActive(false);

            Debug.Log("You rearrange your inventory");
            currentItemIndex--;
            if (currentItemIndex < 0)
                currentItemIndex = Inventory.Length - 1;

            if (Inventory[currentItemIndex] != null)
            {
                Inventory[currentItemIndex].gameObject.SetActive(true);
                Inventory[currentItemIndex].transform.localPosition = new Vector3(0, 4, 7);
                //Debug.Log("You are now holding a ") + Inventory[currentItemIndex].name);

            }
            else
            {
                Debug.Log("You are not holding anything!");
            }
        }


        //E for pick up
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Inventory[currentItemIndex] != null)
                Debug.Log("your hands are full!");
            else
            {
                Collider[] overlappingItems;
                overlappingItems = Physics.OverlapBox(transform.position + 2 * Vector3.forward, 3 * Vector3.one, Quaternion.identity, LayerMask.GetMask("Item"));

                if (overlappingItems.Length == 0)
                    Debug.Log("No item in front of you");
                else
                {
                    //pick up
                    if (Inventory[currentItemIndex] != null)
                    {

                        Inventory[currentItemIndex].transform.SetParent(null);
                        Inventory[currentItemIndex] = null;
                    }

                    Inventory[currentItemIndex] = overlappingItems[0].GetComponent<Item>();
                    Inventory[currentItemIndex].transform.SetParent(gameObject.transform);
                    Inventory[currentItemIndex].transform.localPosition = new Vector3(0, 4, 7);
                    Debug.Log("You picked up a " + Inventory[currentItemIndex].name);
                }

            }
        }

        //G for Drop
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Inventory[currentItemIndex] != null)
            {
                Inventory[currentItemIndex].transform.SetParent(null);
                Debug.Log("You dropped " + Inventory[currentItemIndex].name);
                Inventory[currentItemIndex] = null;
            }
            else
            {
                Debug.Log("You don't have anything to drop!");
            }
        }

        //Space for use
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Inventory[currentItemIndex] != null)
                Inventory[currentItemIndex].GetComponent<Item>().Use();
            else
                Debug.Log("No cant do!!");
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
        Destroy(bullet, 5f);

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
                GameObject.Find("Canvas").transform.Find("GameOverText").gameObject.SetActive(true);
                Application.Quit();
            }

        }
    }
}
