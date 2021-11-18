using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Image healthbar;

    public void UpdateHP(float fraction)
    {
        healthbar.fillAmount = fraction;
    }
}
