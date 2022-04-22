using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isHuman;
    public string marker;
    public static string markerX = "X";
    public static string markerZero = "0";
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

    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += CreateTwoPlayers;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= CreateTwoPlayers;
    }

    public void CreateTwoPlayers(string buttonText)
    {
        Player human = new Player(true, buttonText);
        Player pc = new Player(false, markerX);

        if (buttonText.Equals(markerX)) pc.marker = markerZero;
    }
}
