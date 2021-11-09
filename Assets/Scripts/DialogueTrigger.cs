using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    dialogue dialogue;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] Image avatarSprite;
    [SerializeField] Image textboxSprite;
    [SerializeField] Image avatarSpriteBACK;


    public bool haveTriggered = false;
    public bool triggered = false;
    public bool first = false;
   public bool dialogueComplete = false;


    [SerializeField] Queue<string> sentences;
    [SerializeField] Queue<string> names; //a list of strings
    [SerializeField] Queue<Sprite> avatars;
    [SerializeField] Queue<Sprite> textboxs;

    //GameObject player;

    void Start()
    {

        names = new Queue<string>();
        sentences = new Queue<string>();
        avatars = new Queue<Sprite>();
        textboxs = new Queue<Sprite>();

        avatarSprite.enabled = false;
        textboxSprite.enabled = false; //disable without dialogue
        avatarSpriteBACK.enabled = false;
 
    }

    private void triggerConversation()
    {
        if (FindObjectOfType<Move>().triggered && haveTriggered == false&& !dialogueComplete)
        {
            Debug.Log("trigger conversation");
            haveTriggered = true;
            sentences.Clear();
            names.Clear();
            int randDiag = Random.Range(0, gameObject.GetComponent<dialogues>().myDialogues.Length);
            dialogue = FindObjectOfType<dialogues>().myDialogues[randDiag];
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            foreach (string name in dialogue.names)
            {
                names.Enqueue(name);
            }

            foreach (Sprite avatar in dialogue.avatars)
            {
                avatars.Enqueue(avatar);
            }

            foreach (Sprite textbox in dialogue.textboxs)
            {
                textboxs.Enqueue(textbox);
            }

            Debug.Log("Trigger conversation " + names.Peek());
            triggered = true;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            triggered = false;
            EndDialogue();
        }
    }*/

    void EndDialogue()
    {
        Debug.Log("End conversation ");// + names.Peek());
        avatarSprite.enabled = false;
        textboxSprite.enabled = false;
        avatarSpriteBACK.enabled = false;
        //avatarSprite.gameObject.SetActive(false);
        //textboxSprite.gameObject.SetActive(false);
        nameText.text = "";
        dialogueText.text = "";
        avatarSprite.GetComponent<Image>().sprite = null;
        textboxSprite.GetComponent<Image>().sprite = null;  //clear dialogue stuff after dialogue
        names.Clear();
        sentences.Clear();
        avatars.Clear();
        textboxs.Clear();
        triggered = false;
        dialogueComplete = true;
        haveTriggered = false;
        //FindObjectOfType<playerMove>().speed = 7;
        //FindObjectOfType<playerMove>().jumpHeight = FindObjectOfType<playerMove>().jumpheightInput;     //unfreeze player

    }


    void Update()
    {

        triggerConversation();
        if (triggered && !dialogueComplete )
        {
            if (first == false)
            {
   //freeze player during dialogue
                string name = names.Dequeue();
                string sentence = sentences.Dequeue();
                Sprite avatar = avatars.Dequeue();
                Sprite textbox = textboxs.Dequeue();    //go down list and put into a sprite/string
                avatarSprite.enabled = true;
                textboxSprite.enabled = true;   //show image
                avatarSpriteBACK.enabled = true;
                //avatarSprite.gameObject.SetActive(true);
                //textboxSprite.gameObject.SetActive(true);
                nameText.text = name;
                dialogueText.text = sentence;
                avatarSprite.GetComponent<Image>().sprite = avatar;
                textboxSprite.GetComponent<Image>().sprite = textbox;   //input sprite/string onto placeholders in canvas
                Debug.Log(name);
                Debug.Log(sentence);
                first = true;
                //dialogueComplete = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                   //freeze player during dialogue
       
                if (sentences.Count == 0)   //if queue empty, end dialogue
                {
                    Debug.Log("end Dialogue");
                    dialogueComplete = true;
                    EndDialogue();
                    return;
                }
                //dialogueComplete = true;
                string name2 = names.Dequeue();
                string sentence2 = sentences.Dequeue();
                Sprite avatar2 = avatars.Dequeue();
                Sprite textbox2 = textboxs.Dequeue();    //go down list and put into a sprite/string
                avatarSprite.enabled = true;
                textboxSprite.enabled = true;   //show image
                //avatarSprite.gameObject.SetActive(true);
                //textboxSprite.gameObject.SetActive(true);
                nameText.text = name2;
                dialogueText.text = sentence2;
                avatarSprite.GetComponent<Image>().sprite = avatar2;
                textboxSprite.GetComponent<Image>().sprite = textbox2;   //input sprite/string onto placeholders in canvas
                Debug.Log(name2);
                Debug.Log(sentence2);
            }
        }
    }



}
