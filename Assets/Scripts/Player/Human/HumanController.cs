using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : PlayerController
{
    private void OnEnable()
    {
        Game.TicTacToeController.BoardController.OnBoardCreated += CreatePlayer;
    }
    public override void CreatePlayer(string actualMarker)
    {
        Game.TicTacToeModel.HumanModel.Marker = actualMarker;
        Game.TicTacToeModel.HumanModel.PlayerWins.AddRange(Game.TicTacToeModel.BoardModel.WinCombinations);
        Game.TicTacToeController.BoardController.OnBoardCreated -= CreatePlayer;
        foreach (var cell in Game.TicTacToeModel.BoardModel.CurrentCellList)
        {
            cell.OnPlayerClick += UpdatePlayer;
        }
    }


    public override void UpdatePlayer(CellButton cell)
    {
        Game.TicTacToeModel.HumanModel.PlayerTurns.Add(cell);
        CheckWinCombinations(Game.TicTacToeModel.PCModel.PlayerWins, cell);
        CheckRemainingWins();
        LaunchWinnerDetection(Game.TicTacToeModel.HumanModel);

        if (Game.TicTacToeModel.GameModel.FinishedGame)
        {
            foreach (var c in Game.TicTacToeModel.BoardModel.CurrentCellList)
            {
                c.OnPlayerClick -= UpdatePlayer;
            }
        }
        else
        {
            Game.TicTacToeController.PCController.GeneratePCTurn();
        }
    }
}
