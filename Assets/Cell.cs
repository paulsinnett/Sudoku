using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Image image;
	public Text text;

	int number;

	bool highlight = false;

	[System.Serializable]
	public class KeyToText
	{
		public KeyCode keycode;
		public int number;
	}

	public KeyToText[] keyData;

	public void OnPointerExit (PointerEventData eventData)
	{
		image.color = Color.white;
		highlight = false;
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		image.color = Color.cyan;
		highlight = true;
	}

	void Update()
	{
		if (highlight)
		{
			foreach (KeyToText keyToText in keyData)
			{
				if (Input.GetKeyDown(keyToText.keycode))
				{
					number = keyToText.number;
				}
			}
		}

		text.text = (number == 0)? "" : number.ToString();
	}

	public bool IsEmpty ()
	{
		return number == 0;
	}

	public void SetNumber (int i)
	{
		number = i;
	}

	public void Clear ()
	{
		number = 0;
	}

	public bool IsNumber (int testNumber)
	{
		return number == testNumber;
	}
}
