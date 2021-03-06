﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharObject : MonoBehaviour {

    public Text defText;
	public Text charInButtons;
	public char character;
	public Text text;
	public Image image;
	public RectTransform recTransform;
	public int index;

	[Header("Appearance")]
	public Color normalColor;
	public Color selectedColor;

	bool isSelected = false;

	public CharObject Init(char c)
	{
		character = c;
		text.text = c.ToString ();
		gameObject.SetActive (true);
		return this;
	}


	public Text inputField;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addChars(string c)
	{
		c = charInButtons.text;
		clearInputField ();
		inputField.text = (inputField.text + c).ToString ();
        defText.text = (defText.text + c).ToString();
	}
	public void clearInputField()
	{
		if(inputField.text== "Ilagay ang salitang makikita...")
		{
			inputField.text = "";
		}
        if (defText.text == "kumakandili")
        {
            defText.text = "";
        }
	}
	public void clearInputBox()
	{
		clearInputField ();
		inputField.text = "Ilagay ang salitang makikita...";
        defText.text = "";
	}
}
