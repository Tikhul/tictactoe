using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PCController : PlayerController
{
    private void OnEnable()
    {
        BoardController.OnBoardCreated += CreatePlayer;
        HumanController.OnHumanTurn += GeneratePCTurn;
        CellButton.OnPlayerClick += UpdatePlayer;
    }
    private void OnDisable()
    {
        HumanController.OnHumanTurn -= GeneratePCTurn;
        CellButton.OnPlayerClick -= UpdatePlayer;
    }

    public override void CreatePlayer(string actualMarker)
// Создаю игрока ПК
    {
        if (actualMarker.Equals(PlayerModel.PlayerMarker.X.ToString())) 
        {
            Game.PCModel.Marker = PlayerModel.PlayerMarker.O.ToString();
        }
        else
        {
            Game.PCModel.Marker = PlayerModel.PlayerMarker.X.ToString();
            GeneratePCTurn();
        }
        Game.PCModel.PlayerWins.AddRange(Game.BoardModel.WinCombinations);
        BoardController.OnBoardCreated -= CreatePlayer;
    }

    public override void UpdatePlayer(CellButton cell)
    {
        CheckWinCombinations(Game.PCModel.PlayerWins, cell);
        LaunchWinnerDetection(Game.PCModel);
        CheckRemainingWins();
        Game.PCModel.PlayerTurns.Add(cell);
    }

    public static event System.Action<CellButton> OnPCTurn = delegate { };

    public void GeneratePCTurn()
    {
        Service.BlockButtons();
        IEnumerator coroutine = WaitPCTurn(2.0f);
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitPCTurn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PCStrategy strategy = new PCStrategy();
        strategy.ChooseStrategy();
        OnPCTurn(strategy.ChosenButton);
    }
}

