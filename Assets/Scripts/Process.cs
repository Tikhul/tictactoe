using UnityEngine;
using System;

public class Process : Player
{
    public static Player human;
    public static Player pc;

    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += StartGame;
        CellButton.OnPlayerClick += GeneratePCTurn;
        CellButton.OnPlayerClick += AfterClick;
        CellButton.OnPCTaken += AfterClick;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= StartGame;
        CellButton.OnPlayerClick -= GeneratePCTurn;
        CellButton.OnPlayerClick -= AfterClick;
        CellButton.OnPCTaken -= AfterClick;
    }

    void StartGame(string actualMarker)
    {
        human = new Player(true, actualMarker);
        pc = new Player(false, markerX);

        if (actualMarker.Equals(markerX)) pc.marker = markerZero;

        CreateBoard();

        human.playerWins.AddRange(winCombinations);
        pc.playerWins.AddRange(winCombinations);

        PlayGame(actualMarker);
    }

    void PlayGame(string actualMarker)
    {
        if (actualMarker.Equals(markerZero)) GeneratePCTurn(actualMarker, 1, 'A');
    }

    void AfterClick(string actualMarker, int cellInt, char cellChar)
    {
        // Удаляю занятые клетки
        CellsAfterTurn(cellInt, cellChar);

        // Удаляю недоступные выигрышные комбинации, добавляю ход к игроку
        if (actualMarker.Equals(human.marker))
        {
            pc.playerWins = CheckWinCombinations(pc.playerWins, cellInt, cellChar);
            human.playerTurns.Add(cellInt.ToString() + cellChar.ToString());
        }
        else if (actualMarker.Equals(pc.marker))
        {
            human.playerWins = CheckWinCombinations(human.playerWins, cellInt, cellChar);
            pc.playerTurns.Add(cellInt.ToString() + cellChar.ToString());
        }

        // При достаточном количестве ходов проверяю игру на выигрыш (человек)
        if (human.playerTurns.Count >= boardSettings.rowNumber / 2)
        {
            foreach (var win in human.playerWins)
            {
                int score = 0;
                foreach (var turn in human.playerTurns)
                {
                    if (win.Contains(turn[0].ToString()) && win.Contains(turn[1].ToString())) score++;
                    
                }
                Debug.Log("Human " + score);
                if (score == boardSettings.rowNumber)
                {
                    human.isWinner = true;
                    Debug.Log("Human wins");
                }
            }
        }

        // При достаточном количестве ходов проверяю игру на выигрыш (пк)
        if (pc.playerTurns.Count >= boardSettings.rowNumber / 2)
        {
            foreach (var win in pc.playerWins)
            {
                int score = 0;

                foreach (var turn in pc.playerTurns)
                {
                    if (win.Contains(turn[0].ToString()) && win.Contains(turn[1].ToString())) score++;
      
                }
                Debug.Log("PC" + score);
                if (score == boardSettings.rowNumber)
                {
                    human.isWinner = true;
                    Debug.Log("Pc wins");
                }
            }
        }
    } 

    void FinishGame()
    {

    }
}
