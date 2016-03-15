using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TargetControl : MonoBehaviour {

	public List<Sprite> gesture = new List<Sprite>();
	public string currentName = "";
	public Image currentSprite;


	public void Init()
	{
		GetNextGesture();
    }

	public void GetNextGesture()
	{
		Sprite obj = gesture[Random.Range(0, gesture.Count)];
		currentName = obj.texture.name;
		currentSprite.sprite = obj;
    }
	
}
