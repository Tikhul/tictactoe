using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : TicTacToeElement
{
    public delegate void HumanTurnAction();
    public static event HumanTurnAction OnHumanTurn;
    private void OnEnable()
    {
        BoardController.OnBoardCreated += CreateHumanPlayer;
    }
    private void CreateHumanPlayer(string actualMarker)
    {
        game.human.Marker = actualMarker;
        game.human.PlayerWins.AddRange(game.boardModel.WinCombinations);
        BoardController.OnBoardCreated -= CreateHumanPlayer;
    }
    private void GetHumanTurn(CellButton cell)
    {
     //   if (!game.gameStateController.finishedGame) OnHumanTurn?.Invoke();
    }
}
