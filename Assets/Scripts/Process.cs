using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process : Player
{
    public static Player human;
    public static Player pc;

    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += StartGame;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= StartGame;
    }

    void StartGame(string actualMarker)
    {
        human = new Player(true, actualMarker);
        pc = new Player(false, markerX);

        if (actualMarker.Equals(markerX)) pc.marker = markerZero;

        CreateBoard();

        //human.playerWins = winCombinations;
        //pc.winList = board.winCombinations;
        //cellList = board.cellList;

        //if (marker.Equals(Player.markerZero)) GeneratePCTurn();
    }
}
