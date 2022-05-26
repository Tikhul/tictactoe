using System.Collections;
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

        if (Game.PCModel.PlayerTurns.Count <= Game.BoardModel.BoardSettings.rowNumber / 2)
        {
            int rnd = Random.Range(1, 2);
            if (rnd == 1 && Game.BoardModel.BoardSettings.rowNumber % 2 != 0)
            {
                FillCenterStrategy();
            }
            else if (rnd == 2)
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
            if (Game.HumanModel.PlayerWins.Count - Game.PCModel.PlayerWins.Count > 6)
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
            else if (Game.PCModel.PlayerWins.Count >= 1 && !alarm)
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
            else if (Game.PCModel.PlayerWins.Count >= 1 && alarm)
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

    private void RandomStrategy()
    {
        Debug.Log("RandomStrategy");
        ChosenButton = Game.BoardModel.CellList[Service.RandomInt(Game.BoardModel.CellList.Count)];
    }

    private void FillCenterStrategy()
    {
        Debug.Log("FillCenterStrategy");
        decimal i = Game.BoardModel.BoardSettings.rowNumber / 2;
        int centerIndex = (int)System.Math.Round(i);
        int centerChar = Service.Alphabet[centerIndex];
        ChosenButton = Game.BoardModel.CellList.Single(c => c.CellInt.Equals(centerIndex) && c.CellChar.Equals(centerChar) && !c.Taken);

        if (!ChosenButton) FillDiagonalStrategy();
    }

    private void FillDiagonalStrategy()
    {
        Debug.Log("FillDiagonalStrategy");
        List<CellButton> availableDiagonals = GetAvailableDiagonals();
        if (availableDiagonals.Any())
        {
            ChosenButton = availableDiagonals[Service.RandomInt(availableDiagonals.Count)];
        }
        else
        {
            RandomStrategy();
        }
    }

    private void WinStrategy()
    {
        Debug.Log("WinStrategy");
        List<List<CellButton>> actualWins = SortedWins(Game.PCModel.PlayerWins);
        ChosenButton = actualWins[0].First(c => !c.Taken);
    }

    private void FailHumanStrategy()
    {
        Debug.Log("FailHumanStrategy");
        List<List<CellButton>> humanWins = SortedWins(Game.HumanModel.PlayerWins);
        ChosenButton = humanWins[0].Single(c => !c.Taken);
        alarm = false;
    }
    private List<CellButton> GetAvailableDiagonals()
    {
        List<CellButton> diagonals = new List<CellButton>();
        int rowNumber = Game.BoardModel.BoardSettings.rowNumber;

        foreach (CellButton cell in Game.BoardModel.CellList.FindAll(c => !c.Taken))
        {
            if (cell.CellInt.Equals(0) && cell.CellChar.Equals(Service.Alphabet[0]) ||
                cell.CellInt.Equals(0) && cell.CellChar.Equals(Service.Alphabet[rowNumber - 1]) ||
                cell.CellInt.Equals(rowNumber - 1) && cell.CellChar.Equals(Service.Alphabet[0]) ||
                cell.CellInt.Equals(rowNumber - 1) && cell.CellChar.Equals(Service.Alphabet[rowNumber - 1])
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
        List<List<CellButton>> humanWins = SortedWins(Game.HumanModel.PlayerWins);

        if (humanWins[0].FindAll(c => !c.Taken).Count == 1)
        {
            alarm = true;
        }
    }
}
