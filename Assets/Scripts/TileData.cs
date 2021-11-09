using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    //public int tileSpeed;
    public int gridNum;
    public int gridX;
    public int gridY;

    private void OnMouseDown()
    {
        Debug.Log(gridX + " , " + gridY);
    }
}
