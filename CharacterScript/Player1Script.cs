using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Script : MonoBehaviour {

    public Image hpBar;
    public GameObject StatesUp;

    private ScrambleWord wordCheck;

    public string attack;
    public InputField textBox;
    public Image TimerP1;
    bool actionStarted;
    public BaseCharacter Player1;

    Animator animPlayer1;

    private BattleStateMachine bsm;
    public GameObject targetEnemy;
    private float animSpeed = 90f;

    private float min_Timer = 0;
    private float max_Timer = 10f;

    private Player2Script player2;
    public float AttackSpeed;
    public float RangeSpeed;
    Vector3 StartPosition;
    public enum Turnstate
    {
        processing,
        addToList,
        waiting,
        selecting,
        action,
        dead
    }

    public Turnstate currentState;


	// Use this for initialization
	void Start () {
        wordCheck = GameObject.FindGameObjectWithTag("P1Manager").GetComponent<ScrambleWord>();

        StatesUp.gameObject.SetActive(false);
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();
        StartPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        animPlayer1 = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Animator>();
        targetEnemy = GameObject.FindGameObjectWithTag("Player2");
        bsm = GameObject.FindGameObjectWithTag("BSM").GetComponent<BattleStateMachine>();
        currentState = Turnstate.processing;
	}
	
	// Update is called once per frame
	void Update () {
        hpBar.fillAmount = Player1.cur_HP;
        switch (currentState)
        {
            case(Turnstate.processing):
                Timer10Sec();
                break;
            case (Turnstate.addToList):
                ChooseAction();
                currentState = Turnstate.action;
                break;
            case (Turnstate.waiting):
                break;
            case (Turnstate.selecting):
                break;
            case (Turnstate.action):
                StartCoroutine(TimeForAction());
                break;
            case (Turnstate.dead):
                break;
        }
	}

    //Melee Attack
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //CheckWord Stringlength to base the damage
            Vector3 Player2Position = new Vector3(targetEnemy.transform.position.x - .5f, targetEnemy.transform.position.y, targetEnemy.transform.position.z);
            while (MoveTowardsEnemy(Player2Position)) { yield return null; }
            animPlayer1.SetInteger("state", 1);
            yield return new WaitForSeconds(AttackSpeed);
            doDamage();
            animPlayer1.SetInteger("state", 0);

            Vector3 firstPosition = StartPosition;

            while (MoveTowardsStart(firstPosition)) { yield return null; }
            try
            {
                bsm.PerformList.RemoveAt(0);
            }
            catch
            {
                Debug.Log(bsm.PerformList.Count);
            }


        bsm.battleState = BattleStateMachine.PerformAction.wait;

        actionStarted = false;

        min_Timer = 0;
        currentState = Turnstate.processing;
    }

    private bool MoveTowardsEnemy(Vector3 Target)
    {
        return Target != (transform.position = Vector3.MoveTowards(transform.position, Target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 Target)
    {
        return Target != (transform.position = Vector3.MoveTowards(transform.position, Target, animSpeed * Time.deltaTime));
    }

    void Timer10Sec()
    {
        min_Timer = min_Timer + Time.deltaTime;
        float cacl_Timer = min_Timer / max_Timer;
        TimerP1.transform.localScale = new Vector3(Mathf.Clamp(cacl_Timer, 0, 1), TimerP1.transform.localScale.y, TimerP1.transform.localScale.z);
        if (min_Timer >= max_Timer)
        {
            if (textBox.text == "" || textBox.text == "Ilagay ang salitang makikita...")
            {
                player2.currentState = Player2Script.Turnstate.processing;
                currentState = Turnstate.waiting;
                min_Timer = 0;
            }
            else
            {
                Debug.Log("Pass p2");
                currentState = Turnstate.addToList;
            }
        }
    }
    void ChooseAction()
    {
        HandleTurns myAttack = new HandleTurns();
        myAttack.Attacker = Player1.CharacterName;
        myAttack.Type = "Player";
        myAttack.AttackGameObject = this.gameObject;
        myAttack.AttackersTarget = bsm.P2[Random.Range(0, bsm.P2.Count)];
        bsm.CollectActions(myAttack);
    }

    public void takeDamage(float getDamageAmount)
    {
        animPlayer1.SetInteger("state", 3);
        Player1.cur_HP -= getDamageAmount;
        float health = Player1.cur_HP / Player1.HP;
        if (Player1.cur_HP <= 0)
        {
            animPlayer1.SetInteger("state", 5);
            currentState = Turnstate.dead;
        }
        hpBar.fillAmount = Player1.cur_HP;
    }

    void doDamage()
    {
        float cacl_damage = Player1.Min_Damage;

        targetEnemy.GetComponent<Player2Script>().takeDamage(cacl_damage);
    }
}
