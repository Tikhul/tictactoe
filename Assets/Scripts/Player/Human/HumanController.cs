using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : PlayerController
{
    private void OnEnable()
    {
        Game.TicTacToeController.BoardController.OnBoardCreated += CreatePlayer;
        Game.TicTacToeController.PCController.OnPCTurn += UpdatePlayer;
    }
    private void OnDisable()
    {
        
    }
    public override void CreatePlayer(string actualMarker)
    {
        Game.TicTacToeModel.HumanModel.Marker = actualMarker;
        Game.TicTacToeModel.HumanModel.PlayerWins.AddRange(Game.TicTacToeModel.BoardModel.WinCombinations);
        Game.TicTacToeController.BoardController.OnBoardCreated -= CreatePlayer;
    }


    public override void UpdatePlayer(CellButton cell)
    {
        CheckWinCombinations(Game.TicTacToeModel.PCModel.PlayerWins, cell);
        CheckRemainingWins();
        LaunchWinnerDetection(Game.TicTacToeModel.HumanModel);
        Game.TicTacToeModel.HumanModel.PlayerTurns.Add(cell);
        if (Game.TicTacToeModel.GameModel.FinishedGame)
        {
            Game.TicTacToeController.PCController.OnPCTurn -= UpdatePlayer;
        }
    }
}
