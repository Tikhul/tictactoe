using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process : MonoBehaviour

{
    Board board = new Board();
    public Player human;
    public Player pc;
    public List<CellButton> cellList;
    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += StartGame;
       // CellButton.OnCellHumanClicked += GeneratePCTurn;
       // CellButton.OnCellHumanClicked += CellsAfterTurn;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= StartGame;
       // CellButton.OnCellHumanClicked -= GeneratePCTurn;
       // CellButton.OnCellHumanClicked -= CellsAfterTurn;
    }

    void StartGame(string marker)
    {
        human = new Player(true, marker);
        pc = new Player(false, Player.markerX);

        if (marker.Equals(Player.markerX)) pc.marker = Player.markerZero;

        board.CreateBoard();

        human.winList = board.winCombinations;
        pc.winList = board.winCombinations;
        cellList = board.cellList;

        if (marker.Equals(Player.markerZero)) GeneratePCTurn();
    }

    void GeneratePCTurn()
    {

    }

    public void CellsAfterTurn()
        // клики + ходы пк
    {
        foreach (var cell in cellList)
        {
            if (cell.taken) cellList.Remove(cell);
        }
    }

    public List<string> CheckWinCombinations(List<string> wins, int cellInt, char cellChar) 
    {
        foreach (var w in wins)
        {
            if (w.Contains(cellInt.ToString()) && w.Contains(cellChar.ToString())) wins.Remove(w);
        }
        return wins;
    }

    void FinishGame()
    {
        // Если нет больше выигрышных стратегий
    }
}
