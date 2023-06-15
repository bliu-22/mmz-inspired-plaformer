using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    PlayerStats playerStats;
    [SerializeField] float test;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        playerStats = FindObjectOfType<PlayerStats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerStats.GetHealth();
        
    }
}
