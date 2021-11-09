using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Move : MonoBehaviour
{
    public float speed;         //create a variable float speed 

    public bool upNow = false;
    public bool downNow = false;
    public bool leftNow = false;
    public bool rightNow = false;
    public AudioSource myAudio;
    public AudioSource houseAudio;
    public AudioClip audio0;
    public AudioClip audio1;
    public AudioClip audio2;
    public AudioClip audio3;
    public bool havePlayed0 = false;
    public bool havePlayed1 = false;
    public bool havePlayed2 = false;
    public bool havePlayed3 = false;
    public float myvolume;
    bool low=false;
    public bool triggered = false;
    public float money = 0;
    public GameObject dialoguetrigger;


    public TMP_Text moneyText;





    Vector3 orgiPos;
    // Start is called before the first frame update
    void Start()
    {
        //myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialoguetrigger.GetComponent<DialogueTrigger>().haveTriggered)
        {
            MoveChar(); //call the fuction
        }
        
        playerMove();
        if (myAudio.volume < myvolume && !low)
        {
            myAudio.volume += 0.1f;
        }
        moneyText.text = "red pocket money: " + money;
    }

    void MoveChar()
    {
        Vector3 newPos = transform.position;        //get the object's original position
        if (!upNow && !downNow && !leftNow && !rightNow)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))          //if input key uparrow
            {
                upNow = true;
                orgiPos = transform.position;
                //newPos.y += speed;// * Time.deltaTime;     //increase y according to speed
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))   //if input key downArrow
            {
                downNow = true;
                orgiPos = transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) // if input rightArrow
            {
                rightNow = true;
                orgiPos = transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))    // if input leftArrow
            {
                leftNow = true;
                orgiPos = transform.position;
            }
        }
        transform.position = newPos;                //put object's position to this new position
    }

    void playerMove()
    {
        Vector3 thisPos = transform.position;

        if (upNow == true)
        {
            if (thisPos.y - orgiPos.y < 1)
            {
                thisPos.y += 2f * Time.deltaTime;
            }
            else if (thisPos.y - orgiPos.y >= 1)
            {
                thisPos.y = orgiPos.y + 1;
                upNow = false;
            }
        }
        else if (downNow== true)
        {
            if (orgiPos.y - thisPos.y < 1)
            {
                thisPos.y -= 2f * Time.deltaTime;
            }
            else if (orgiPos.y - thisPos.y >= 1)
            {
                thisPos.y = orgiPos.y - 1;
                downNow = false;
            }
        }
        if (rightNow == true)
        {
            if (thisPos.x - orgiPos.x < 1)
            {
                thisPos.x += 2f * Time.deltaTime;
            }
            else if (thisPos.x - orgiPos.x >= 1)
            {
                thisPos.x = orgiPos.x + 1;
                rightNow = false;
            }
        }
        else if (leftNow == true)
        {
            if (orgiPos.x - thisPos.x < 1)
            {
                thisPos.x -= 2f * Time.deltaTime;
            }
            else if (orgiPos.x - thisPos.x >= 1)
            {
                thisPos.x = orgiPos.x - 1;
                leftNow = false;
            }
        }
        //Debug.Log(thisPos.y - orgiPos.y);
        //Debug.Log(thisPos.x - orgiPos.x);
        transform.position = thisPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "house")
        {
            low = true;
            myAudio.volume = 0.25f;
            Debug.Log("play music");
            houseAudio.Play();
            triggered = true;
        }
        if (collision.tag == "tile")
        {
            
            collision.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            Debug.Log(collision.GetComponent<SpriteRenderer>().color);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "house")
        {
            Debug.Log("stop music");
            houseAudio.Stop();
            low = false;
            triggered = false;
            FindObjectOfType<DialogueTrigger>().triggered = false;
            FindObjectOfType<DialogueTrigger>().first = false;
            FindObjectOfType<DialogueTrigger>().dialogueComplete = false;
            money += Random.Range(100, 1000);
            Debug.Log(money);
        }
        if (collision.tag == "tile")
        {
            collision.GetComponent<SpriteRenderer>().color = new Color(195, 195, 195, 0.6f);
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // speed = collision.GetComponent<TileData>().tileSpeed;
        if (collision.tag == "tile")
        {
            collision.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            if (collision.GetComponent<TileData>().gridNum == 0)
            {
                havePlayed1 = false;
                havePlayed2 = false;
                havePlayed3 = false;
                myAudio.clip = audio0;
                if (!havePlayed0)
                {
                    if (!low)
                    {
                        myAudio.volume = 0;
                    }
                    myAudio.Play();
                    havePlayed0 = true;
                }
            }

            if (collision.GetComponent<TileData>().gridNum == 1)
            {
                havePlayed0 = false;
                havePlayed2 = false;
                havePlayed3 = false;
                myAudio.clip = audio1;
                if (!havePlayed1)
                {
                    if (!low)
                    {
                        myAudio.volume = 0;
                    }
                    myAudio.Play();
                    havePlayed1 = true;
                }
            }

            if (collision.GetComponent<TileData>().gridNum == 2)
            {
                havePlayed0 = false;
                havePlayed1 = false;
                havePlayed3 = false;
                myAudio.clip = audio2;
                if (!havePlayed2)
                {
                    if (!low)
                    {
                        myAudio.volume = 0;
                    }
                    myAudio.Play();
                    havePlayed2 = true;
                }
            }

            if (collision.GetComponent<TileData>().gridNum == 3)
            {
                havePlayed0 = false;
                havePlayed1 = false;
                havePlayed2 = false;
                myAudio.clip = audio3;
                if (!havePlayed3)
                {
                    if (!low)
                    {
                        myAudio.volume = 0;
                    }
                    myAudio.Play();
                    havePlayed3 = true;
                }
            }
        }
    }

    




}
