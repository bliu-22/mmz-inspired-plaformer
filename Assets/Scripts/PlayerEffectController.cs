using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField] GameObject playerTrail;
    [SerializeField] GameObject dustEffect, shockwaveEffect;
    [SerializeField] float interval;
    bool generateTrails;
    float remaningTime;
    private void Start()
    {
        remaningTime = interval;
    }

    private void Update()
    {
        if (generateTrails) 
        {
            if (remaningTime <= 0)
            {
                GameObject playerTrailInstance = Instantiate(playerTrail, transform.position, Quaternion.identity);
                playerTrailInstance.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                playerTrailInstance.transform.localScale = transform.localScale;
                remaningTime = interval;
            }
            else
            {
                remaningTime -= Time.deltaTime;
            }
        }

    }
    public void PlayDashEffects() 
    {
        GameObject shockwaveEffectInstance = Instantiate(shockwaveEffect, transform.position + Vector3.up * 0.2f + Vector3.right * transform.localScale.x * 0.4f, Quaternion.identity);
        shockwaveEffectInstance.transform.localScale = transform.localScale;
        GameObject dustEffectInstance = Instantiate(dustEffect, transform.position, Quaternion.identity);
        dustEffectInstance.transform.localScale = transform.localScale;
    }
    public void ActivateTrails()
    {
        generateTrails = true;
    }
    public void DisableTrails() 
    {
        generateTrails = false;
    }
}
