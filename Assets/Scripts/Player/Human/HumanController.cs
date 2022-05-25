using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : PlayerController
{
    private void OnEnable()
    {
        BoardController.OnBoardCreated += CreateHumanPlayer;
        PCController.OnGenerateFinished += GetHumanTurn;
    }
    private void OnDisable()
    {
        PCController.OnGenerateFinished -= GetHumanTurn;
    }
    private void CreateHumanPlayer(string actualMarker)
    {
        game.human.Marker = actualMarker;
        game.human.PlayerWins.AddRange(game.boardModel.WinCombinations);
        BoardController.OnBoardCreated -= CreateHumanPlayer;
    }
    public delegate void HumanTurnAction();
    public static event HumanTurnAction OnHumanTurn;
    private void GetHumanTurn(CellButton cell)
    {
        Service.ActivateButtons();
        UpdateHuman(cell);
        if (!game.gameModel.FinishedGame) OnHumanTurn?.Invoke();
    }
    private void UpdateHuman(CellButton cell)
    {
        CheckWinCombinations(game.human.PlayerWins, cell);
        game.human.PlayerTurns.Add(cell);
    }
}
