using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform effectiveArea;
    [SerializeField] GameObject text; 
    [SerializeField] Vector2 areaSize;
    bool playerDetected;
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected) 
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!CheckKey())
                {
                    text.GetComponent<TextManager>().ChangeText("You don't have the key");
                }
                else 
                {
                    Collider2D[] objectsCollided = Physics2D.OverlapBoxAll(effectiveArea.position, areaSize, 0);
                    if (objectsCollided != null)
                    {
                        foreach (Collider2D obj in objectsCollided)
                        {
                            if (obj.gameObject.CompareTag("Player"))
                            {
                                rb.velocity = new Vector2(rb.velocity.x, 3f);
                                isMoving = true;
                                //text.SetActive(false);
                                text.GetComponent<TextManager>().DisableText();
                            }

                        }
                    }
                }
                
            }
        }

    }

    private bool CheckKey() 
    {
        return PlayerInventory.inventory.ContainsKey("Key");
        //return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.GetComponent<TextManager>().ChangeText("Press ↑ to use the elevator");
            //text.SetActive(true);
            text.GetComponent<TextManager>().EnableText();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            text.GetComponent<TextManager>().DisableText();
            //text.SetActive(false);
            //text.GetComponent<TextManager>().ChangeText("Press ↑ to use the elevator");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerDetected = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerDetected = false; 
    }
  
    private void OnDrawGizmos()
    {
        //visualized area where player can activate the elevator
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(effectiveArea.position, areaSize);
    }
}
