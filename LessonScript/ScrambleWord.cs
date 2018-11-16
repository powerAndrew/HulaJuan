using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScrambleWord : MonoBehaviour {

    private Player1Script player1;
    private Player2Script player2;

	[Header("Other Reference")]
	public Button btnSubmit;
	public Button btnNewWord;

	[Header("UI REFERENCE")]
	public int comboScore;
	public CharObject prefab; 
	public Transform container;
	public int currentWord ;
	public string[] words;
	List<CharObject> charObjects = new List<CharObject> ();

	//Player 1 Jumbled Letters
	List<string> Player1Correct1 = new List<string> () {"pinagaaral", "pagaaral", "aral", "gara","piga", "gapi","gana", "pila","pana", "inaral",
		"pagal", "aaralin","nilaga"};
	List<string> Player1Correct2 = new List<string> () {"kumakandili", "kandila", "dila", "kama","daan", "kain","kumakain", "makina","manika"};
	List<string> Player1Correct3 = new List<string> () {"dalubulnungan", "daungan", "laba", "ganda","nadala", "bulungan","bagal", "baga"};
	List<string> Player1Correct4 = new List<string> () {"balusuplingan", "bali", "supling", "linga","libag", "alis","asal", "banga", "bansa", "bilugan"
		, "lupain"};

	//Player 2 Jumble Letters
	List<string> Player2Correct2 = new List<string> () {"itinatangis", "tangis", "tangi", "tinatangis","angas", "anit","angat", "atis","gatas", "gata",
		"gisa", "gasta","gasa", "tinaga"};
	List<string> Player2Correct1 = new List<string> () {"sulatroniko", "sulat", "tula", "ulat","suki", "tuka","suka", "siko","trono", "aklat",
		"tono"};
	List<string> Player2Correct3 = new List<string> () {"talipandas", "tali", "landas", "patas","antas", "sandal","tanda", "daan", "dati"};
	List<string> Player2Correct4 = new List<string> () {"nagkukumahog", "guho", "handog", "kumag","kama", "mangga","umaga","hamog","goma"};
	
	public Text txtChoices;

	// Use this for initialization
	void Start () {
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2Script>();
        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1Script>();
        int startingWord = Random.Range(0, 3);
        currentWord = startingWord;
        int indexChar = Random.Range (0, words.Length - 1);
		//txtChoices.text = words[indexChar].ToString ();
		showScrambleLetters (currentWord);
	}
	
	// Update is called once per frame
	void Update () {
		RepositionObject ();
		if (prefab.inputField.text != "" && prefab.inputField.text != "Ilagay ang salitang makikita...")
		{
			btnSubmit.enabled = true;
		}
		else
		{
			btnSubmit.enabled = false;
		}


	}
	void RepositionObject()
	{
		if(charObjects.Count ==  0)	
		{
			return;
		}

		for (int i = 0; i <charObjects.Count; i++)
		{
			charObjects [i].recTransform.anchoredPosition = charObjects[i].recTransform.anchoredPosition;

			charObjects [i].index = i;
		}
	}
	public char[] chars;
	CharObject clone;
	public void showScrambleLetters(int index)
	{
		charObjects.Clear ();
		foreach(Transform child in container)
		{
			Destroy ((child.gameObject));
		}
		chars = words [index].ToCharArray ();

		foreach(char c in chars)
		{
			 clone = Instantiate (prefab.gameObject).GetComponent<CharObject> ();
			clone.transform.SetParent (container);
			charObjects.Add ((clone.Init (c)));
			clone.enabled = true;
		}
		currentWord = index;
	}
	public void clearInputBox()
	{
		showScrambleLetters (currentWord);
		prefab.clearInputField ();
		prefab.inputField.text = "Ilagay ang salitang makikita...";
	}
	public void newWords()
	{
		currentWord++;
		showScrambleLetters (currentWord);
	}

	public void checkWord()
	{
		if(currentWord == 0)
		{
			if(Player1Correct1.Contains (prefab.inputField.text))
			{
				comboScore++;
				Debug.Log ("Word Correct1");
			}
			else
			{
				comboScore=0;
				Debug.Log ("Word Incorrect!");
			}
		}else if(currentWord ==1)
		{
			if(Player1Correct2.Contains (prefab.inputField.text))
			{
				comboScore++;
				Debug.Log ("Word Correct2");
			}
			else
			{
				comboScore=0;
				Debug.Log ("Word Incorrect!");
			}
		}
		else if( currentWord == 2)
		{
			if(Player1Correct3.Contains (prefab.inputField.text))
			{
				comboScore++;
				Debug.Log ("Word Correct3");
			}
			else
			{
				comboScore=0;
				Debug.Log ("Word Incorrect!");
			}
		}
		else if(currentWord == 3)
		{
			if(Player1Correct4.Contains (prefab.inputField.text))
			{
				comboScore++;
				Debug.Log ("Word Correct4");
			}
			else
			{
				comboScore=0;
				Debug.Log ("Word Incorrect!");
			}
		}

		clearInputBox ();


	}

    public void CoCheckWord()
    {
        if (prefab.inputField.text == "aral" || prefab.inputField.text == "gara" || prefab.inputField.text == "piga" || prefab.inputField.text == "gapi" || prefab.inputField.text == "gana"
            || prefab.inputField.text == "pila" || prefab.inputField.text == "pana" || prefab.inputField.text == "dila" || prefab.inputField.text == "kama" || prefab.inputField.text == "daan"
            || prefab.inputField.text == "kain" || prefab.inputField.text == "laba" || prefab.inputField.text == "bali" || prefab.inputField.text == "alis" || prefab.inputField.text == "asal"
            || prefab.inputField.text == "anit" || prefab.inputField.text == "atis" || prefab.inputField.text == "gata" || prefab.inputField.text == "gisa" || prefab.inputField.text == "gasa"
            || prefab.inputField.text == "tula" || prefab.inputField.text == "ulat" || prefab.inputField.text == "suki" || prefab.inputField.text == "tuka" || prefab.inputField.text == "suka"
            || prefab.inputField.text == "suka" || prefab.inputField.text == "siko" || prefab.inputField.text == "tono" || prefab.inputField.text == "tali" || prefab.inputField.text == "dati"
            || prefab.inputField.text == "guho" || prefab.inputField.text == "goma" || prefab.inputField.text == "baga")
        {
            //Remove the correct input into the list to avoid repeatition
            //Damage here

            Player1Correct1.Remove(prefab.inputField.text);
            player2.Player2.cur_HP -= 10f;
            Debug.Log("Easy!");
        }
        else if (prefab.inputField.text == "pagaaral" || prefab.inputField.text == "aaralin" || prefab.inputField.text == "kandila" || prefab.inputField.text == "kumakain" || prefab.inputField.text == "daungan"
            || prefab.inputField.text == "bulungan" || prefab.inputField.text == "tinatangis" || prefab.inputField.text == "aaralin" || prefab.inputField.text == "aaralin" || prefab.inputField.text == "aaralin")
        {
            //Damage Here
            Debug.Log("Close enaugh!");
            player1.currentState = Player1Script.Turnstate.action;
            player2.Player2.cur_HP -= 15f;
        }
        else if (prefab.inputField.text == "inaral" || prefab.inputField.text == "nilaga" || prefab.inputField.text == "pagal" || prefab.inputField.text == "makina" || prefab.inputField.text == "manika"
            || prefab.inputField.text == "ganda" || prefab.inputField.text == "nadala" || prefab.inputField.text == "bagal" || prefab.inputField.text == "linga" || prefab.inputField.text == "libag"
            || prefab.inputField.text == "banga" || prefab.inputField.text == "bansa" || prefab.inputField.text == "bilugan" || prefab.inputField.text == "lupain" || prefab.inputField.text == "tangis"
            || prefab.inputField.text == "tangi" || prefab.inputField.text == "angas" || prefab.inputField.text == "angat" || prefab.inputField.text == "gatas" || prefab.inputField.text == "tinaga"
            || prefab.inputField.text == "sulat" || prefab.inputField.text == "trono" || prefab.inputField.text == "aklat" || prefab.inputField.text == "landas" || prefab.inputField.text == "patas"
            || prefab.inputField.text == "antas" || prefab.inputField.text == "sandal" || prefab.inputField.text == "tanda" || prefab.inputField.text == "handog" || prefab.inputField.text == "kumag"
            || prefab.inputField.text == "mangga" || prefab.inputField.text == "umaga" || prefab.inputField.text == "hamog")
        {
            //Damage here
            Debug.Log("Medium!");
            player1.currentState = Player1Script.Turnstate.action;
            player2.Player2.cur_HP -= 20f;
        }
        else if ( prefab.inputField.text == "kumakandili" || prefab.inputField.text == "dalubulnungan" || prefab.inputField.text == "balusuplingan"
            || prefab.inputField.text == "itinatangis" || prefab.inputField.text == "sulatroniko" || prefab.inputField.text == "talipandas" || prefab.inputField.text == "nagkukumahog")
        {
            //damge here
            Debug.Log("Hard!");
            player1.currentState = Player1Script.Turnstate.action;
            player2.Player2.cur_HP -= 30f;
            if (prefab.inputField.text == "kumakandili")
            {
                prefab.defText.text = "kumakandili - Nagmamalasakit";
            }

        }
        else
        {
            Debug.Log("Mali!");
            player2.currentState = Player2Script.Turnstate.processing;
        }

        clearInputBox();
    }

    void DoDamage()
    {
        int damage3 = 30;
        int damage2 = 20;
        int damage1 = 10;
        if (prefab.inputField.text.Length <= 13 && prefab.inputField.text.Length >=10)
        {
            player2.Player2.cur_HP -= damage3;
        }
        if (prefab.inputField.text.Length <= 9 && prefab.inputField.text.Length <= 6)
        {
            player2.Player2.cur_HP -= damage2;
        }
        if (prefab.inputField.text.Length <= 5 && prefab.inputField.text.Length >= 4)
        {
            player2.Player2.cur_HP -= damage1;
        }
    }
}
