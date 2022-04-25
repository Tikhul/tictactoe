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
        CellButton.OnCellHumanClicked += GeneratePCTurn;
        CellButton.OnCellHumanClicked += AfterTurn;
        CellButton.OnCellPCTaken += AfterTurn;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= StartGame;
        CellButton.OnCellHumanClicked -= GeneratePCTurn;
        CellButton.OnCellHumanClicked -= AfterTurn;
        CellButton.OnCellPCTaken -= AfterTurn;
    }

    void StartGame(string actualMarker)
    {
        human = new Player(true, actualMarker);
        pc = new Player(false, markerX);

        if (actualMarker.Equals(markerX)) pc.marker = markerZero;

        CreateBoard();

        human.winList = winCombinations;
        pc.winList = winCombinations;

        if (pc.marker.Equals(markerX)) GeneratePCTurn(actualMarker, 1, 'A');
    }

    void AfterTurn(string actualMarker, int cellInt, char cellChar)
    {
        CellsAfterTurn(cellInt, cellChar);

        if (actualMarker.Equals(human.marker)) CheckWinCombinations(pc.winList, cellInt, cellChar);
        else if (actualMarker.Equals(pc.marker)) CheckWinCombinations(human.winList, cellInt, cellChar);
    }
}
