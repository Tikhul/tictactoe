using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : PlayerController
{
    private void OnEnable()
    {
        Game.BoardController.OnBoardCreated += CreatePlayer;
        Game.PCController.OnPCTurn += UpdatePlayer;
    }
    private void OnDisable()
    {
        Game.PCController.OnPCTurn -= UpdatePlayer;
    }
    public override void CreatePlayer(string actualMarker)
    {
        Game.HumanModel.Marker = actualMarker;
        Game.HumanModel.PlayerWins.AddRange(Game.BoardModel.WinCombinations);
        Game.BoardController.OnBoardCreated -= CreatePlayer;
    }


    public override void UpdatePlayer(CellButton cell)
    {
        CheckWinCombinations(Game.HumanModel.PlayerWins, cell);
        LaunchWinnerDetection(Game.HumanModel);
        CheckRemainingWins();
        Game.HumanModel.PlayerTurns.Add(cell);
    }
}
