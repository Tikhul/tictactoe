using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerController : TicTacToeElement
{
    public void CreatePlayers(string actualMarker)
    {
        game.human.isHuman = true;
        game.human.marker = actualMarker;

        game.pc.isHuman = false;
        game.pc.marker = PlayerModel.markerX;

        game.human.playerWins.AddRange(game.boardModel.winCombinations);
        game.pc.playerWins.AddRange(game.boardModel.winCombinations);

        if (actualMarker.Equals(PlayerModel.markerX)) game.pc.marker = PlayerModel.markerZero;
    }

    public void UpdatePlayers(PlayerModel player, CellButton cell)
    {
        if (player.isHuman)
        {
            CheckWinCombinations(game.pc.playerWins, cell);
            player.playerTurns.Add(cell);
        }
        else if (!player.isHuman)
        {
            CheckWinCombinations(game.human.playerWins, cell);
            player.playerTurns.Add(cell);
        }
    }

    public void CheckRemainingWins()
    {
        if (!game.human.playerWins.Any() && !game.pc.playerWins.Any())
        {
            game.gameStateController.finishedGame = true;
            game.stepExecutionController.OutOfTurns("Ничья");
        }
    }
    void CheckWinCombinations(List<List<CellButton>> wins, CellButton cell)
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
        float steps = game.boardModel.boardSettings.rowNumber;

        if (player.playerTurns.Count >= steps)
        {
            DetectWinner(player);
        }   
    }

    void DetectWinner(PlayerModel player)
        // Определение победителя при наличии выигрышных комбинаций
    {
        foreach (var win in player.playerWins)
        {
            int score = 0;

            foreach (var turn in player.playerTurns)
            {
                if (win.Contains(turn)) 
                {
                    score++;
                }  
            }

            if (score >= game.boardModel.boardSettings.rowNumber)
            {
                player.isWinner = true;
                game.gameStateController.finishedGame = true;
                if (player.isHuman) game.stepExecutionController.OutOfTurns("Вы выиграли");
                else game.stepExecutionController.OutOfTurns("ПК выиграл");
                break;
            }
        }         
    }
}

