using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreatePlayers : MonoBehaviour
{
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
        Player pc = new Player(false, Player.markerX);

        if (buttonText.Equals(Player.markerX)) pc.marker = Player.markerZero;
    }
}
