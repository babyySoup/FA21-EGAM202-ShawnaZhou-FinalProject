using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int KillCount = 0;
    // Start is called before the first frame update

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 50), "Kill Count: " + KillCount);
    }
}
