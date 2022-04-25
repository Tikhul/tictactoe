using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Board
{
    public bool isHuman;
    public string marker;
    public static string markerX = "X";
    public static string markerZero = "0";
    public bool IsHuman { get; set; }
    public List<string> winList;
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

    public delegate void GenerateTurnAction(CellButton button);
    public static event GenerateTurnAction OnPCTurn;
    public void GeneratePCTurn(string marker, int cellInt, char cellChar)
    {
        IEnumerator coroutine = WaitPCTurn(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitPCTurn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        System.Random rand = new System.Random();
        int index = rand.Next(cellList.Count);
        CellButton chosenButton = cellList[index];
        OnPCTurn(chosenButton);
    }
}
