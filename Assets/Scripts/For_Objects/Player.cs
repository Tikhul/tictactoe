using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Board
{
    public bool isHuman;
    public string marker;
    public static string markerX = "X";
    public static string markerZero = "0";
    public List<string> playerWins;
    public bool IsHuman { get; set; }
    public string Marker
    {
        get { return marker; }
        set
        {
            if (value.Equals(markerZero) || value.Equals(markerX)) marker = value; 
        }
    }

    public Player()
    {

    }
    public Player(bool isHuman, string marker)
    {
        IsHuman = isHuman;
        Marker = marker;
    }

    public delegate void GenerateAction(CellButton button);
    public static event GenerateAction OnTurnGenerated;
    public void GeneratePCTurn(string actualMarker, int cellInt, char cellChar)
    {
        IEnumerator coroutine = WaitPCTurn(1.0f);
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitPCTurn(float waitTime)
    {
        
        yield return new WaitForSeconds(waitTime);
        foreach (var i in cellList) Debug.Log("Player " + i.cellInt.ToString()+i.cellChar.ToString());
        System.Random rnd = new System.Random();
        if (cellList.Count > 0)
        {
            int r = rnd.Next(cellList.Count);
            Debug.Log("Index" + r);
            Debug.Log("Count" + cellList.Count);
            CellButton chosenButton = cellList[r];
            OnTurnGenerated(chosenButton);
        }
    }

}
