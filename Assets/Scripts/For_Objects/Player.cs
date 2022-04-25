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
}
