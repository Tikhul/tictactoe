using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class PlayerController : TicTacToeElement
{
    public abstract void CreatePlayer(string actualMarker);
    public abstract void UpdatePlayer(CellButton cell);
    protected void CheckRemainingWins()
    // Проверяю, не закончились ли выигрышные комбинации
    {
        if (!Game.TicTacToeModel.HumanModel.PlayerWins.Any() && !Game.TicTacToeModel.PCModel.PlayerWins.Any())
        {
            Game.TicTacToeController.GameController.CheckGameState(true);
            Game.TicTacToeController.GameController.GetResults("Ничья");
        }
    }
    protected void CheckWinCombinations(List<List<CellButton>> wins, CellButton cell)
    // Убираю недоступные выигрышные комбинации для игрока
    {
        List<List<CellButton>> tempList = new List<List<CellButton>>();

        foreach (var win in wins)
        {
            if (win.Contains(cell)) 
            {
                tempList.Add(win);
            }
        }
        wins.RemoveAll(item => tempList.Contains(item));
    }

    protected void LaunchWinnerDetection(PlayerModel player)
    // При достаточном количестве ходов проверяю игру на выигрыш
    {
        float steps = Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber;

        if (player.PlayerTurns.Count >= steps)
        {
            Debug.Log("LaunchWinnerDetection");
            DetectWinner(player);
        }   
    }

    private void DetectWinner(PlayerModel player)
    // Определение победителя при наличии выигрышных комбинаций
    {
        Debug.Log("Playe ");
        foreach (var win in player.PlayerWins)
        {
            int score = 0;

            foreach (var turn in player.PlayerTurns)
            {
                if (win.Contains(turn)) 
                {
                    score++;
                }  
            }

            if (score >= Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber)
            {
                player.IsWinner = true;
                Game.TicTacToeController.GameController.CheckGameState(true);
                if (player.IsHuman) Game.TicTacToeController.GameController.GetResults("Вы выиграли");
                else Game.TicTacToeController.GameController.GetResults("ПК выиграл");
                break;
            }
        }         
    }
}

