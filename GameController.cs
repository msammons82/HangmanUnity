using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    public Text timeField;
    public GameObject[] hangMan;
    public GameObject winText;
    public GameObject loseText;
    public GameObject replayButton;
    public Text wordToFindField;
    private float time;
 //private string[] wordsLocal = {"MATT","JOANNE","ROBERT","MARY JANE","DENNIS" };
    private string[] words = File.ReadAllLines(@"Assets/Words.txt");
    private string chosenWord;
    private string hiddenWord;
    private int fails; 
    private bool gameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        chosenWord = words[Random.Range(0,words.Length)];
        
        for (int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            if(char.IsWhiteSpace(letter))
            {
                hiddenWord += " ";
            }
            else
            {
                hiddenWord += "_";
            }

        }
        
        wordToFindField.text = hiddenWord;
        Debug.Log(chosenWord);

    }
   
    // Update is called once per frame
    void Update()
    {
        if(gameEnd == false)
        {
            time += Time.deltaTime;
            timeField.text = time.ToString();
        }
        
    }

    private void OnGUI() 
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString();

            Debug.Log("Keydown event was triggered " + pressedLetter);
            //check if pressed letter is contained in chosen word
            if(chosenWord.Contains(pressedLetter))
            {
                int i = chosenWord.IndexOf(pressedLetter);
                while(i != -1)
                {
                    //set new hidden word to everything before the i, 
                    //change the i to the letter pressed, and everything after the i//
                    hiddenWord = hiddenWord.Substring(0,i) + pressedLetter + hiddenWord.Substring(i + 1);

                    chosenWord = chosenWord.Substring(0, i) + "_" + chosenWord.Substring(i + 1);

                    i = chosenWord.IndexOf(pressedLetter);
                }
           
                wordToFindField.text = hiddenWord;
           
            }
            // add a hang man body part 
            else
            {
                hangMan[fails].SetActive(true);
                fails++;
            }
            if(fails == hangMan.Length)
            {
                loseText.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }
            if(!hiddenWord.Contains("_"))
            {
                winText.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }
        }
    }

}
