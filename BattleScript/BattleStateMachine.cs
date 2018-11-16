using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour {

    public Image TimerP1;
    private Player1Script Player1;
    private Player2Script Player2;

    public enum PerformAction
    {
        wait,
        takeAction,
        performAction
    }

    public PerformAction battleState;

    public List<HandleTurns> PerformList = new List<HandleTurns>();

    public List<GameObject> P1 = new List<GameObject>();
    public List<GameObject> P2 = new List<GameObject>();
    public
	// Use this for initialization
	void Start () {
        battleState = PerformAction.wait;
        Player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1Script>();
        Player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();


        P1.Add(GameObject.FindGameObjectWithTag("Player"));
        P2.Add(GameObject.FindGameObjectWithTag("Player2"));
    }
	
	// Update is called once per frame
	void Update () {

        switch (battleState)
        {
            case(PerformAction.wait):
                if (PerformList.Count > 0)
                {
                    battleState = PerformAction.takeAction;
                }
                break;
            case (PerformAction.takeAction):
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                if (PerformList[0].Type == "Player2")
                {
                    Player2Script plyr2 = performer.GetComponent<Player2Script>();
                    plyr2.targetEnemy = PerformList[0].AttackersTarget;
                    plyr2.currentState = Player2Script.Turnstate.action;
                }
                if (PerformList[0].Type == "Player")
                {
                    Player1Script plyr = performer.GetComponent<Player1Script>();
                    plyr.targetEnemy = PerformList[0].AttackersTarget;
                    plyr.currentState = Player1Script.Turnstate.action;
                    
                }
                break;

            case (PerformAction.performAction):

                break;
        }

        battleState = PerformAction.performAction;
	}

    public void CollectActions(HandleTurns input)
    {
        PerformList.Add(input);
    }
}
