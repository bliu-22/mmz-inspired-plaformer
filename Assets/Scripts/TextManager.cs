using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextManager : MonoBehaviour
{
    TMPro.TextMeshProUGUI message;
    // Start is called before the first frame update
    void Start()
    {
        message = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void ChangeText(string content) 
    {
        message.text = content;
    }
    public void EnableText() 
    {
        message.enabled = true;
    }
    public void EnableText(float duration) 
    {
        message.enabled = true;
        Invoke("DisableText", duration);
    }
    public void DisableText() 
    {
        message.enabled = false;
    }
}
