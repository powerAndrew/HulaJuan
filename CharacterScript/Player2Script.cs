using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Script : MonoBehaviour {

    public Image hpBar;

    public GameObject StatesUp;

    public BaseEnemy Player2;

    bool actionStarted;

    
    public InputField textBox;

    Animator animPlayer1;

    private Player1Script player1;

    public Image TimerP2;
    private float min_Timer = 0;
    private float max_Timer = 10f;

    private BattleStateMachine bsm;
    public GameObject targetEnemy;
    private float animSpeed = 90;


    public float AttackSpeed;
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

        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1Script>();
        StatesUp.gameObject.SetActive(false);
        StartPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        animPlayer1 = GameObject.FindGameObjectWithTag("Player2").gameObject.GetComponent<Animator>();
        targetEnemy = GameObject.FindGameObjectWithTag("Player");
        bsm = GameObject.FindGameObjectWithTag("BSM").GetComponent<BattleStateMachine>();
        currentState = Turnstate.waiting;
	}

    // Update is called once per frame
    void Update()
    {

        
        switch (currentState)
        {
            case (Turnstate.processing):
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

    public void CheckWord()
    {

    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //CheckWord Stringlength to base the damage
            Vector3 Player1Position = new Vector3(targetEnemy.transform.position.x + 1.5f, targetEnemy.transform.position.y, targetEnemy.transform.position.z);
            while (MoveTowardsEnemy(Player1Position)) { yield return null; }

            yield return new WaitForSeconds(AttackSpeed);
            //doDamage();

            Vector3 firstPosition = StartPosition;

            while (MoveTowardsStart(firstPosition)) { yield return null; }

            bsm.PerformList.RemoveAt(0);

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
        //this function timer must be remove after checking the word and trigger the action function
        min_Timer = min_Timer + Time.deltaTime;
        float cacl_Timer = min_Timer / max_Timer;
        TimerP2.transform.localScale = new Vector3(Mathf.Clamp(cacl_Timer, 0, 1), TimerP2.transform.localScale.y, TimerP2.transform.localScale.z);
        if (min_Timer >= max_Timer)
        {
            if (textBox.text == "" || textBox.text == "Ilagay ang salitang makikita...")
            {
                currentState = Turnstate.waiting;
                StopCoroutine(TimeForAction());
                player1.currentState = Player1Script.Turnstate.processing;
                min_Timer = 0;
            }
            else
            {
                Debug.Log("Pass p1");
                currentState = Turnstate.addToList;
            }
        }
    }

    void ChooseAction()
    {
        HandleTurns myAttack = new HandleTurns();
        myAttack.Attacker = Player2.EnemyName;
        myAttack.AttackGameObject = this.gameObject;
        myAttack.AttackersTarget = bsm.P1[(Random.Range(0, bsm.P1.Count))];
        bsm.CollectActions(myAttack);
    }

    public void takeDamage(float getDamageAmount)
    {
        animPlayer1.SetInteger("state", 3);
        Player2.cur_HP -= getDamageAmount / Player2.cur_HP;
        float health = Player2.cur_HP / Player2.HP;
        if (Player2.cur_HP <= 0)
        {
            animPlayer1.SetInteger("state", 5);
            currentState = Turnstate.dead;
        }
        hpBar.fillAmount = Player2.cur_HP;

        animPlayer1.SetInteger("state", 0);
    }

    void doDamage()
    {
        float cacl_damage = Player2.Min_Damage;

        targetEnemy.GetComponent<Player1Script>().takeDamage(cacl_damage);
    }
}
