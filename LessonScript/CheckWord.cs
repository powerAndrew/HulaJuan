using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWord : MonoBehaviour {

    Player1Script playerScript;
    int randomDamage;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1Script>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void sendDamageInfo()
    {
        
          
    }
}
