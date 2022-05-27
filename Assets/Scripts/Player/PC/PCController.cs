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
        //CellButton.OnPlayerClick += UpdatePlayer;
    }
    private void OnDisable()
    {
       
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
        foreach(var cell in Game.TicTacToeModel.BoardModel.CellList)
        {
            cell.OnPlayerClick += UpdatePlayer;
        }
    }

    public override void UpdatePlayer(CellButton cell)
    {
        CheckWinCombinations(Game.TicTacToeModel.HumanModel.PlayerWins, cell);
        CheckRemainingWins();
        LaunchWinnerDetection(Game.TicTacToeModel.PCModel);
        Game.TicTacToeModel.PCModel.PlayerTurns.Add(cell);
        if (!Game.TicTacToeModel.GameModel.FinishedGame) GeneratePCTurn();
    }


    public void GeneratePCTurn()
    {
        Game.TicTacToeController.BoardController.ManageButtons(false);
        IEnumerator coroutine = WaitPCTurn(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitPCTurn(float waitTime)
    {
        
        yield return new WaitForSeconds(waitTime);
        PCStrategy strategy = new PCStrategy();
        strategy.ChooseStrategy();
        if (!Game.TicTacToeModel.GameModel.FinishedGame)
        {
            OnPCTurn?.Invoke(strategy.ChosenButton);
            Game.TicTacToeController.BoardController.ManageButtons(true);
        }
        else
        {
            //CellButton.OnPlayerClick -= UpdatePlayer;
        }
    }
}

