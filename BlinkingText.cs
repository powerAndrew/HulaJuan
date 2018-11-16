using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlinkingText : MonoBehaviour {

	public GameObject Blink;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("blinkTheText", 0f, 1f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			SceneManager.LoadScene ("Game01");


	}
	void blinkTheText()
	{
		if(Blink.activeInHierarchy)
		{
			Blink.SetActive (false);
		} 
		else
		{
			Blink.SetActive(true);
		}
	}
}
