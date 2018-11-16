using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player1.Player1.cur_HP -= 15;
        }
        if (other.gameObject.tag == "Player2")
        {
            player1.Player1.cur_HP -= 15;
        }
    }
}
