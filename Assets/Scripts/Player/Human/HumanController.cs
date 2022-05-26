using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : PlayerController
{
    private void OnEnable()
    {
        BoardController.OnBoardCreated += CreatePlayer;
        CellButton.OnPlayerClick += GetHumanTurn;
        PCController.OnPCTurn += ActivateBoard;
    }
    private void OnDisable()
    {
        CellButton.OnPlayerClick -= GetHumanTurn;
        PCController.OnPCTurn -= ActivateBoard;
    }
    public override void CreatePlayer(string actualMarker)
    {
        Game.HumanModel.Marker = actualMarker;
        Game.HumanModel.PlayerWins.AddRange(Game.BoardModel.WinCombinations);
        BoardController.OnBoardCreated -= CreatePlayer;
    }

    public static event System.Action OnHumanTurn = delegate { };
    private void GetHumanTurn(CellButton cell)
    {
        UpdatePlayer(cell);
        if (!Game.GameModel.FinishedGame) OnHumanTurn?.Invoke();
    }
    public override void UpdatePlayer(CellButton cell)
    {
        CheckWinCombinations(Game.HumanModel.PlayerWins, cell);
        LaunchWinnerDetection(Game.HumanModel);
        CheckRemainingWins();
        Game.HumanModel.PlayerTurns.Add(cell);
    }
    private void ActivateBoard(CellButton cell)
    {
        Service.ActivateButtons();
    }
}
