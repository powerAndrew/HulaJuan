using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {
	public string sceneToLoad;

	public GameObject Blink;

	public GameObject panelInstruc;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("blinkTheText", 0f, 1f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			panelInstruc.SetActive (true);


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
	public void startGame()
	{
		SceneManager.LoadScene (sceneToLoad);
	}
}
