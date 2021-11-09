using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create new variable type "Dialogue" that include a string for name and a stringlist for dialogue

[System.Serializable] //allow input 
public class dialogue
{
    public string[] names;
    public Sprite[] avatars;
    public Sprite[] textboxs;

    [TextArea(3, 10)]    //personalize the size of textbox in unity editor for a bigger textbox
    public string[] sentences;
}
