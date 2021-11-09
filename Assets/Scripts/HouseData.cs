using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseData : MonoBehaviour
{
    public bool triggered = false;
    public int gridNum;
    // Start is called before the first frame update
    void Start()
    {
        gridNum = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.name == "Player")
        {
            triggered = true;
            Debug.Log("trigger House");
        }*/
    }
}
