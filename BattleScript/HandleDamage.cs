using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDamage : MonoBehaviour {

    private Player1Script player1;
    private Player2Script player2;

	// Use this for initialization
	void Start () {
        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1Script>();
        player2 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2Script>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
