using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public GameObject TimerText;
    public int hoursLeft = 24;
    public bool takingAway = false;

    void Start()
    {
        TimerText.GetComponent<Text>().text = "Hours remaining:" + hoursLeft;
    }

    private void Update()
    {
        if(takingAway == false && hoursLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
        
        //win condition 
        if (takingAway == false && hoursLeft < 1)
        {
            TimerText.GetComponent<Text>().text = "You have completed your jorney at the Dark City.";
            Application.Quit();
        }
    }
    IEnumerator TimerTake()
    {
        takingAway = true;
        //every 15 seconds is 1 hour in game 
        yield return new WaitForSeconds(15);
        hoursLeft -= 1;
        TimerText.GetComponent<Text>().text = "Hours remaining:" + hoursLeft;
        takingAway = false; 
    }
}
