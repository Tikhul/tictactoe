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
        CellButton.OnPlayerClick += GeneratePCTurn;
        CellButton.OnPCTaken += AfterClick;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= StartGame;
        CellButton.OnPlayerClick -= AfterClick;
        CellButton.OnPlayerClick -= GeneratePCTurn;
        CellButton.OnPCTaken -= AfterClick;
    }

    void StartGame(string actualMarker)
    {
        human = new Player(true, actualMarker);
        pc = new Player(false, markerX);

        if (actualMarker.Equals(markerX)) pc.marker = markerZero;

        CreateBoard();
        human.playerWins = winCombinations;
        pc.playerWins = winCombinations;

        if (actualMarker.Equals(markerZero)) GeneratePCTurn(actualMarker, 1, 'H');
    }

    public delegate void GenerateAction(CellButton button);
    public static event GenerateAction OnTurnGenerated;

    void GeneratePCTurn(string actualMarker, int cellInt, char cellChar)
    {
        System.Random rnd = new System.Random();
        int r = rnd.Next(cellList.Count);
        CellButton chosenButton = cellList[r];
        OnTurnGenerated(chosenButton);
    }

    void AfterClick(string actualMarker, int cellInt, char cellChar)
    {
        if (actualMarker.Equals(human.marker))
            CheckWinCombinations(pc.playerWins, cellInt, cellChar);
        else 
            CheckWinCombinations(human.playerWins, cellInt, cellChar);

        CellsAfterTurn();
    }
}
