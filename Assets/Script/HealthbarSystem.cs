using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathbarSystem : MonoBehaviour
{
    private Image Healthbar;
    public float hp;
    private float MaxHealth;


    Survival Player;

    private void Start()
    {
        Healthbar = GetComponent<Image>();
        Player = FindObjectOfType<Survival>();
    }
    
    private void Update()
    {
        Healthbar.fillAmount = hp / MaxHealth;
        hp = Player.hp;
    }
}
