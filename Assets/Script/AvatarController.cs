using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public GameObject Mousebox;
    public Animator thisAnimator;
    public UnityEngine.AI.NavMeshAgent thisNavMeshAgent;

    public enum MovementStateT { StandingStill, walking }
    public MovementStateT currentState;

    //for items
    public Item[] Inventory;
    public int currentItemIndex;

    // Start is called before the first frame update
    private void Start()
    {
        thisAnimator = GetComponent<Animator>();
        thisNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        currentState = MovementStateT.StandingStill;
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(currentItemIndex)*/;


        Ray rayFromCameraToMouse;
        RaycastHit closestClickableGroundOnRay;

        rayFromCameraToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayFromCameraToMouse, out closestClickableGroundOnRay, Mathf.Infinity, LayerMask.GetMask("ClickableGround"));
        Mousebox.transform.position = closestClickableGroundOnRay.point;

        if (Input.GetMouseButton(0))
            Mousebox.transform.localScale = new Vector3(5, 5, 5);
        else
            Mousebox.transform.localScale = new Vector3(3, 3, 3);

        if (Input.GetMouseButton(0))
            thisNavMeshAgent.SetDestination(Mousebox.transform.position);

        //animator speed
        thisAnimator.SetFloat("WalkSpeed", 1f * thisNavMeshAgent.velocity.magnitude);



        //up arrow
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

        //R to Wear item 
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Inventory[currentItemIndex] != null)
                Inventory[currentItemIndex].GetComponent<Item>().Wear();
            else
                Debug.Log("No cant do!!");
        }

        //Q to eat
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Inventory[currentItemIndex] != null)
                Inventory[currentItemIndex].GetComponent<Item>().Eat();
            else
                Debug.Log("No cant do!!");
        }
    }
  
}
