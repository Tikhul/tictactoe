﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PCStrategy : TicTacToeElement
{
    private bool alarm = false;
    public CellButton ChosenButton { get; set; }
    public void ChooseStrategy()
    {
        DetectAlarm();

        if (Game.TicTacToeModel.PCModel.PlayerTurns.Count <= Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber / 2)
        {
            int rnd = Random.Range(1, 2);
            //if (rnd == 1 && Game.BoardModel.BoardSettings.rowNumber % 2 != 0)
            //{
            //    FillCenterStrategy();
            //}
            /*else */if (rnd == 2)
            {
                FillDiagonalStrategy();
            }
            else
            {
                RandomStrategy();
            }
        }
        else
        {
            if (Game.TicTacToeModel.HumanModel.PlayerWins.Count - Game.TicTacToeModel.PCModel.PlayerWins.Count > 6)
            {
                int rnd = Random.Range(1, 3);
                if (rnd == 1 || rnd == 2)
                {
                    FailHumanStrategy();
                }
                else
                {
                    WinStrategy();
                }
            }
            else if (Game.TicTacToeModel.PCModel.PlayerWins.Count >= 1 && !alarm)
            {
                int rnd = Random.Range(1, 3);

                if (rnd == 1 || rnd == 2)
                {
                    WinStrategy();
                }
                else
                {
                    RandomStrategy();
                }
            }
            else if (Game.TicTacToeModel.PCModel.PlayerWins.Count >= 1 && alarm)
            {
                int rnd = Random.Range(1, 3);

                if (rnd == 1 || rnd == 2)
                {
                    FailHumanStrategy();
                }
                else
                {
                    WinStrategy();
                }
            }
        }
    }

    public void RandomStrategy()
    {
        Debug.Log("RandomStrategy");
        System.Random rnd = new System.Random();
        ChosenButton = Game.TicTacToeModel.BoardModel.CellList[rnd.Next(Game.TicTacToeModel.BoardModel.CellList.Count)];
    }

    private void FillCenterStrategy()
    {
        Debug.Log("FillCenterStrategy");
        decimal i = Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber / 2;
        int centerIndex = (int)System.Math.Round(i);
        char centerChar = BoardModel.Alphabet[centerIndex];
        ChosenButton = Game.TicTacToeModel.BoardModel.CellList.Single(c => c.CellInt.Equals(centerIndex) && c.CellChar.Equals(centerChar) && !c.Taken);

        if (ChosenButton == null) FillDiagonalStrategy();
    }

    private void FillDiagonalStrategy()
    {
        Debug.Log("FillDiagonalStrategy");
        List<CellButton> availableDiagonals = GetAvailableDiagonals();
        if (availableDiagonals.Any())
        {
            System.Random rnd = new System.Random();
            ChosenButton = availableDiagonals[rnd.Next(availableDiagonals.Count)];
        }
        else
        {
            RandomStrategy();
        }
    }

    private void WinStrategy()
    {
        Debug.Log("WinStrategy");
      //  Debug.Log(Game.TicTacToeModel.PCModel.PlayerWins.Count);
        List<List<CellButton>> actualWins = SortedWins(Game.TicTacToeModel.PCModel.PlayerWins);
        ChosenButton = actualWins[0].First(c => !c.Taken);
    }

    private void FailHumanStrategy()
    {
        Debug.Log("FailHumanStrategy");
    //    Debug.Log(Game.TicTacToeModel.PCModel.PlayerWins.Count);
        List<List<CellButton>> humanWins = SortedWins(Game.TicTacToeModel.HumanModel.PlayerWins);
        ChosenButton = humanWins[0].Single(c => !c.Taken);
        alarm = false;
    }
    private List<CellButton> GetAvailableDiagonals()
    {
        List<CellButton> diagonals = new List<CellButton>();
        int rowNumber = Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber;

        foreach (CellButton cell in Game.TicTacToeModel.BoardModel.CellList.FindAll(c => !c.Taken))
        {
            if (cell.CellInt.Equals(0) && cell.CellChar.Equals(BoardModel.Alphabet[0]) ||
                cell.CellInt.Equals(0) && cell.CellChar.Equals(BoardModel.Alphabet[rowNumber - 1]) ||
                cell.CellInt.Equals(rowNumber - 1) && cell.CellChar.Equals(BoardModel.Alphabet[0]) ||
                cell.CellInt.Equals(rowNumber - 1) && cell.CellChar.Equals(BoardModel.Alphabet[rowNumber - 1])
                )
            {
                diagonals.Add(cell);
            }
        }
        return diagonals;
    }

    private List<List<CellButton>> SortedWins(List<List<CellButton>> receivedWins)
    {
        WinsComparer wc = new WinsComparer();
        List<List<CellButton>> actualWins = receivedWins;

        actualWins.Sort(wc);
        return actualWins;
    }
    private void DetectAlarm()
    {
        List<List<CellButton>> humanWins = SortedWins(Game.TicTacToeModel.HumanModel.PlayerWins);

        if (humanWins.Any() && humanWins[0].FindAll(c => !c.Taken).Count == 1)
        {
            alarm = true;
        }
    }
}
