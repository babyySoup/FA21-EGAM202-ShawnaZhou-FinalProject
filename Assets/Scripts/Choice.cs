using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour
{

    public GameObject TextBox;
    public GameObject Choice1;
    public GameObject Choice2;
    public GameObject Choice3;
    public int choiceMade;

    public void Dialog1()
    {
        TextBox.GetComponent<Text>().text = "There are hungry creatures in every corner of this city.";
        choiceMade = 1;
    }

    public void Dialog2()
    {
        TextBox.GetComponent<Text>().text = "You don't need to know more about me, unless you are here for busniess.";
        choiceMade = 2;
    }

    public void Dialog3()
    {
        TextBox.GetComponent<Text>().text = "Bye bye, Don't get chewed up.";
        choiceMade = 3;
    }


    void Update()
    {
        if(choiceMade >= 1)
        {
            Choice1.SetActive(false);
            Choice2.SetActive(false);
            Choice3.SetActive(false);
        }
    }
}
