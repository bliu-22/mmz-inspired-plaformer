using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected string itemName = "null";
    [SerializeField] GameObject text;

    public virtual void Start()
    {
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (PlayerInventory.inventory.ContainsKey(itemName))
            {
                PlayerInventory.inventory[itemName] += 1;
            }
            else 
            {
                PlayerInventory.inventory.Add(itemName, 1);
            }
            text.GetComponent<TextManager>().ChangeText("[" + itemName + "]" + " obtained");
            text.GetComponent<TextManager>().EnableText(3f);

            Destroy(gameObject);

        }
    }
}
