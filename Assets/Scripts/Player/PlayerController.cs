using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerController : TicTacToeElement
{
    public virtual void CreatePlayer(string actualMarker) { }
    public virtual void UpdatePlayer(CellButton cell) { }
    protected void CheckRemainingWins()
    // Проверяю, не закончились ли выигрышные комбинации
    {
        if (!Game.HumanModel.PlayerWins.Any() && !Game.PCModel.PlayerWins.Any())
        {
            Game.GameController.CheckGameState(true);
            Game.GameController.GetResults("Ничья");
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
        float steps = Game.BoardModel.BoardSettings.rowNumber;

        if (player.PlayerTurns.Count >= steps)
        {
            Debug.Log("LaunchWinnerDetection");
            DetectWinner(player);
        }   
    }

    private void DetectWinner(PlayerModel player)
    // Определение победителя при наличии выигрышных комбинаций
    {
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

            if (score >= Game.BoardModel.BoardSettings.rowNumber)
            {
                player.IsWinner = true;
                Game.GameController.CheckGameState(true);
                if (player.IsHuman) Game.GameController.GetResults("Вы выиграли");
                else Game.GameController.GetResults("ПК выиграл");
                break;
            }
        }         
    }
}

