using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heathbar : MonoBehaviour
{
    private Image Healthbar;
    public float hp;
    private float MaxHealth;


    private void Start()
    {
        Healthbar = GetComponent<Image>();

    }
    
    public void Update()
    {
        Healthbar.fillAmount = hp / MaxHealth;

    }
}
