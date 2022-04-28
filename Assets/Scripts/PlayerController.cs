﻿using System.Collections;
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

    public void UpdatePlayers(string actualMarker, CellButton cell)
    {
        if (actualMarker.Equals(game.human.marker))
        {
            CheckWinCombinations(game.pc.playerWins, cell);
            game.human.playerTurns.Add(cell.cellChar.ToString() + cell.cellInt.ToString());
        }
        else if (actualMarker.Equals(game.pc.marker))
        {
            CheckWinCombinations(game.human.playerWins, cell);
            game.pc.playerTurns.Add(cell.cellChar.ToString() + cell.cellInt.ToString());
        }
    }

    void CheckWinCombinations(List<string> wins, CellButton cell)
    // Убираю недоступные выигрышные комбинации для игрока
    {
        if (wins.Any())
        {
            List<string> tempList = new List<string>();

            foreach (var win in wins)
            {
                for (int i = 0; i < win.Length - 1; i += 2)
                {
                    if (win[i].ToString().Contains(cell.cellChar.ToString()) && win[i + 1].ToString().Contains(cell.cellInt.ToString()))
                    {

                        tempList.Add(win);
                    }
                }
            }

            // foreach (var t in tempList) Debug.Log(t);
            wins.RemoveAll(item => tempList.Contains(item));
        }
        else 
        {
            game.gameStateController.FinishGame();
            game.finalUI.ActivateResults("Ничья");
        }
        
    }

    public void LaunchWinnerDetection(string actualMarker)
    // При достаточном количестве ходов проверяю игру на выигрыш (человек)
    {
        float halfSteps = game.boardModel.boardSettings.rowNumber / 2;

        if (actualMarker.Equals(game.human.marker) && game.human.playerTurns.Count >= halfSteps)
        {
            Debug.Log("Human detect launch");
            DetectWinner(game.human.playerWins, game.human.playerTurns, game.human);
        }
        else if (actualMarker.Equals(game.pc.marker) && game.pc.playerTurns.Count >= halfSteps)
        {
            Debug.Log("pc detect launch");
            DetectWinner(game.pc.playerWins, game.pc.playerTurns, game.pc);
        }
            
    }

    void DetectWinner(List<string> wins, List<string> turns, PlayerModel player)
    {
        foreach (var win in wins)
        {
            int score = 0;

            foreach (var turn in turns)
            {
                if (win.Contains(turn)) score++;
            }

            if (score == game.boardModel.boardSettings.rowNumber)
            {
                player.isWinner = true;
                game.gameStateController.FinishGame();
                if (player.isHuman) game.finalUI.ActivateResults("Вы выиграли");
                else game.finalUI.ActivateResults("ПК выиграл");
                Debug.Log("winner detected");
            }
        }

                
    }
}

