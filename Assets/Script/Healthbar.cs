using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image HealthbarImage;
    public float hp;
    private float MaxHealth;
    float fillAmount;

    private void Start()
    {
        HealthbarImage = GetComponent<Image>();

    }
    
    public void Update()
    {
        fillAmount = hp / MaxHealth;

    }
}
