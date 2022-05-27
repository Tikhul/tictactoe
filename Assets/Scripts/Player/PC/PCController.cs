using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PCController : PlayerController
{
    public event System.Action<CellButton> OnPCTurn = delegate { };

    private void OnEnable()
    {
        Game.TicTacToeController.BoardController.OnBoardCreated += CreatePlayer;
    }

    public override void CreatePlayer(string actualMarker)
// Создаю игрока ПК
    {
        if (actualMarker.Equals(PlayerModel.PlayerMarker.X.ToString())) 
        {
            Game.TicTacToeModel.PCModel.Marker = PlayerModel.PlayerMarker.O.ToString();
        }
        else
        {
            Game.TicTacToeModel.PCModel.Marker = PlayerModel.PlayerMarker.X.ToString();
            GeneratePCTurn();
        }
        Game.TicTacToeModel.PCModel.PlayerWins.AddRange(Game.TicTacToeModel.BoardModel.WinCombinations);
        Game.TicTacToeController.BoardController.OnBoardCreated -= CreatePlayer;
    }

    public override void UpdatePlayer(CellButton cell)
    {
        Game.TicTacToeModel.PCModel.PlayerTurns.Add(cell);
        CheckWinCombinations(Game.TicTacToeModel.HumanModel.PlayerWins, cell);
        CheckRemainingWins();
        LaunchWinnerDetection(Game.TicTacToeModel.PCModel);
        Debug.Log("PC update");
        Debug.Log("Finished game " + Game.TicTacToeModel.GameModel.FinishedGame);
        //if (!Game.TicTacToeModel.GameModel.FinishedGame) GeneratePCTurn();
    }


    public void GeneratePCTurn()
    {
        Game.TicTacToeController.BoardController.ManageButtons(false);
        IEnumerator coroutine = WaitPCTurn(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitPCTurn(float waitTime)
    {
        if (!Game.TicTacToeModel.GameModel.FinishedGame)
        {
            yield return new WaitForSeconds(waitTime);
            PCStrategy strategy = new PCStrategy();
            strategy.RandomStrategy();
            OnPCTurn?.Invoke(strategy.ChosenButton);
            UpdatePlayer(strategy.ChosenButton);
            Game.TicTacToeController.BoardController.ManageButtons(true);
        }
    }
}

