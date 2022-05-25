using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerController : TicTacToeElement
{
    public void UpdatePlayers(PlayerModel player, CellButton cell)
    {
        if (player.IsHuman)
        {
            CheckWinCombinations(game.pc.PlayerWins, cell);
            player.PlayerTurns.Add(cell);
        }
        else if (!player.IsHuman)
        {
            CheckWinCombinations(game.human.PlayerWins, cell);
            player.PlayerTurns.Add(cell);
        }
    }

    public void CheckRemainingWins()
    // Проверяю, не закончились ли выигрышные комбинации
    {
        if (!game.human.PlayerWins.Any() && !game.pc.PlayerWins.Any())
        {
            game.gameController.CheckGameState(true);
            //game.stepExecutionController.OutOfTurns("Ничья");
        }
    }
    private void CheckWinCombinations(List<List<CellButton>> wins, CellButton cell)
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

    public void LaunchWinnerDetection(PlayerModel player)
    // При достаточном количестве ходов проверяю игру на выигрыш (человек)
    {
        float steps = game.boardModel.BoardSettings.rowNumber;

        if (player.PlayerTurns.Count >= steps)
        {
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

            if (score >= game.boardModel.BoardSettings.rowNumber)
            {
                player.IsWinner = true;
                game.gameController.CheckGameState(true);
                //if (player.IsHuman) game.stepExecutionController.OutOfTurns("Вы выиграли");
                //else game.stepExecutionController.OutOfTurns("ПК выиграл");
                break;
            }
        }         
    }
}

