using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent thisNavMeshAgent;
    //shooting
    public float fireSpeed;
    float timer;
    public GameObject bulletPrefab;
    public Transform shotPoint;

    //movement
    public float moveSpeed;
    public GameObject Mousebox;

    //health 
    public float health;
    public healthbar healthbar;
    private float currentHealth;

    //inventory
    public Item[] Inventory;
    public int currentItemIndex;

    //knife ability 
    public GameObject knifeDance;

    //canvas for chat
    public Behaviour DialogCanvas;

    public bool canChat;
    public float chatRange = 10f;
    public Transform NPC;
    public LayerMask isNPC;
    public GameObject CanvasNPC;

    void Start()
    {
        thisNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        NPC = GameObject.Find("Assassin").transform;
        currentHealth = health;

    }

    void Update()
    {

        Ray rayFromCameraToMouse;
        RaycastHit closestClickableGroundOnRay;

        rayFromCameraToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayFromCameraToMouse, out closestClickableGroundOnRay, Mathf.Infinity, LayerMask.GetMask("Ground"));
        Mousebox.transform.position = closestClickableGroundOnRay.point;

        if (Input.GetMouseButton(1))
            Mousebox.transform.localScale = new Vector3(2, 2, 2);
        else
            Mousebox.transform.localScale = new Vector3(1, 1, 1);

        if (Input.GetMouseButton(1))
            thisNavMeshAgent.SetDestination(Mousebox.transform.position);




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
                Inventory[currentItemIndex].transform.localPosition = new Vector3(0, 1, 1);
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
                Inventory[currentItemIndex].transform.localPosition = new Vector3(0, 1, 1);
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
                    Inventory[currentItemIndex].transform.localPosition = new Vector3(0, 1, 1);
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
            {
                Inventory[currentItemIndex].GetComponent<Item>().Use();
                health += 15f;
                currentHealth = health;
                healthbar.UpdateHealth((float)currentHealth / (float)50f);
            }
            else
                Debug.Log("cant do!!");
        }


            //Q to cast ability "Knife Dance"
            if (Input.GetKeyDown(KeyCode.Q))
        {
            var ability = Instantiate(knifeDance, transform.position, Quaternion.identity);
        }

        
        //F to chat
        //depend on chat range to activate canvas
        canChat = Physics.CheckSphere(transform.position, chatRange, isNPC);

        if (canChat && Input.GetKeyDown(KeyCode.F) && !CanvasNPC.activeSelf)
        {
            Debug.Log("Chatting");
            CanvasNPC.SetActive(true);
        }else if (canChat && Input.GetKeyDown(KeyCode.F) && CanvasNPC.activeSelf)
        {
            Debug.Log("Not Chatting");
            CanvasNPC.SetActive(false);
        }


    }

    
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = shotPoint.forward * 15f;
        Destroy(bullet, 5f);

    }

    //player taking damage
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Creature")
        {
            health -= other.gameObject.GetComponent<Creature>().Damage;
            currentHealth = health;
            healthbar.UpdateHealth((float)currentHealth / (float)50f);

            if (health <= 0)
            {
                //player die
                GameObject.Find("Canvas").transform.Find("GameOverText").gameObject.SetActive(true);
                Application.Quit();
            }

        }
        if (other.gameObject.tag == "CreatureS")
        {
            health -= other.gameObject.GetComponent<CreatureS>().Damage;
            currentHealth = health;
            healthbar.UpdateHealth((float)currentHealth / (float)50f);

            if (health <= 0)
            {
                //player die
                Debug.Log("Hit");
                GameObject.Find("Canvas").transform.Find("GameOverText").gameObject.SetActive(true);
                Application.Quit();
            }
        }
        
            

    }
}

