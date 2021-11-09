using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject housePrefab;
    public Sprite[] tileSprites;
    public Sprite[] houseSprites;

    public int gridWidth;
    public int gridHeight;
    GameObject[,] gridTiles;
    GameObject[,] houseTiles;


    [SerializeField] float ftime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        gridTiles = new GameObject[gridWidth, gridHeight];  //create 10*10 box/slot
        houseTiles = new GameObject[gridWidth, gridHeight];
        StartCoroutine(loopDelay());
    }

    IEnumerator loopDelay()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)    //call out all the inner loop first 
            {
                print("started delay");
                MakeTile(x, y);
                yield return new WaitForSeconds(ftime);     //wait time until next tile    
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeTile(int xPos, int yPos)
    {
        GameObject newTile = Instantiate(tilePrefab);
        GameObject newHouse;
        int randTile = Random.Range(0, tileSprites.Length);             //random int from the array
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[randTile];
        newTile.GetComponent<SpriteRenderer>().color = new Color(195, 195, 195, 0.6f);

        int randHouse = Random.Range(0, 10);
        if (randHouse >= 7 && randHouse <9)
        {
            if (xPos < gridWidth && xPos > 0 && yPos < (gridHeight-1) && yPos > 0) {
                if (houseTiles[xPos, yPos - 1] == null && houseTiles[xPos - 1, yPos] == null && houseTiles[xPos-1, yPos - 1] == null && houseTiles[xPos - 1, yPos + 1] == null) {     //If no house near by
                    newHouse = Instantiate(housePrefab);
                    if (randHouse == 7)
                    {
                        houseTiles[xPos, yPos] = newTile;
                        newHouse.GetComponent<SpriteRenderer>().sprite = houseSprites[0];
                    }
                    if (randHouse == 8)
                    {

                         houseTiles[xPos, yPos] = newTile;
                         newHouse.GetComponent<SpriteRenderer>().sprite = houseSprites[1];
                    }
                    newHouse.transform.position = new Vector3(transform.position.x + xPos, transform.position.y + yPos, 0);  //set position
                }
            }
            /*else
            {
                newHouse = Instantiate(housePrefab);
                if (randHouse == 7)
                {
                    houseTiles[xPos, yPos] = newTile;
                    newHouse.GetComponent<SpriteRenderer>().sprite = houseSprites[0];
                }
                if (randHouse == 8)
                {

                    houseTiles[xPos, yPos] = newTile;
                    newHouse.GetComponent<SpriteRenderer>().sprite = houseSprites[1];
                }
                newHouse.transform.position = new Vector3(transform.position.x + xPos, transform.position.y + yPos, 0);  //set position
            }*/
            
        }
        
        newTile.transform.position = new Vector3(transform.position.x + xPos, transform.position.y + yPos, 0);  //set position
        TileData myData = newTile.GetComponent<TileData>();
        if (randTile == 0)
        {
            myData.gridNum = 0;
        } else if (randTile == 1)
        {
            myData.gridNum = 1;
        }else if (randTile == 2)
        {
            myData.gridNum = 2;
        }else if (randTile == 3)
        {
            myData.gridNum = 3;
        }
        myData.gridX = xPos;
        myData.gridY = yPos;
        gridTiles[xPos, yPos] = newTile;        //put the gameObject in the grid
    }


}
