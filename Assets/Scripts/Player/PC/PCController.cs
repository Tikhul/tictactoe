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
    }


    public void GeneratePCTurn()
    {
        Game.TicTacToeController.BoardController.ManageButtons(false);
        //IEnumerator coroutine = WaitPCTurn(2.0f);
        //StartCoroutine(coroutine);
        StartCoroutine(WaitPCTurn(2f));
    }

    private IEnumerator WaitPCTurn(float waitTime)
    {
        if (!Game.TicTacToeModel.GameModel.FinishedGame)
        {
            yield return new WaitForSeconds(waitTime);
            PCStrategy strategy = new PCStrategy();
            strategy.ChooseStrategy();
            OnPCTurn?.Invoke(strategy.ChosenButton);
            UpdatePlayer(strategy.ChosenButton);

            if (!Game.TicTacToeModel.GameModel.FinishedGame)
            {
                Game.TicTacToeController.BoardController.ButtonsAfterTurn(true);
            }   
        }
    }
}

