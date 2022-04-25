using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Process : Player
{
    public static Player human;
    public static Player pc;

    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += StartGame;
        CellButton.OnPlayerClick += AfterClick;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= StartGame;
        CellButton.OnPlayerClick -= AfterClick;
    }

    void StartGame(string actualMarker)
    {
        human = new Player(true, actualMarker);
        pc = new Player(false, markerX);

        if (actualMarker.Equals(markerX)) pc.marker = markerZero;

        CreateBoard();
        human.playerWins = winCombinations;
        pc.playerWins = winCombinations;
        //if (marker.Equals(Player.markerZero)) GeneratePCTurn();
    }
    //void GeneratePCTurn(string pcMarker)
   // {
      //  System.Random rnd = new System.Random();
     //   int r = rnd.Next(cellList.Count);
   // }

    void AfterClick(string actualMarker, int cellInt, char cellChar)
    {
        if (actualMarker.Equals(human.marker))
            CheckWinCombinations(pc.playerWins, cellInt, cellChar);
        else 
            CheckWinCombinations(human.playerWins, cellInt, cellChar);

        CellsAfterTurn();
    }
}
