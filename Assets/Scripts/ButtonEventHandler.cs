using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Button myText;
	

	void Start()
	{
		myText = GetComponent<Button>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		myText.transform.localScale = new Vector2(1.1f, 1.1f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		myText.transform.localScale = new Vector2(1, 1);
	}

    
}
