using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PCController : TicTacToeElement
{
    private bool alarm = false;
    private void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += LaunchFirstTurn;
    }
    private void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= LaunchFirstTurn;
    }
    private void LaunchFirstTurn(string actualMarker)
    {
        if (actualMarker.Equals(PlayerModel.MarkerZero)) LaunchPCTurn();
    }

    private void LaunchPCTurn()
    {
        Service.BlockButtons();
        game.pcController.GeneratePCTurn();
    }

    public delegate void GenerateAction(CellButtonModel cell);
    public static event GenerateAction OnGenerateFinished;

    public void GeneratePCTurn()
    {
        IEnumerator coroutine = WaitPCTurn(2.0f);
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitPCTurn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ChooseStrategy();
    }

    private void ChooseStrategy()
    {
        DetectAlarm();
        //  Debug.Log(alarm);

        if (game.pc.PlayerTurns.Count <= game.boardModel.BoardSettings.rowNumber / 2)
        {
            int rnd = Random.Range(1, 2);
            if (rnd == 1 && game.boardModel.BoardSettings.rowNumber % 2 != 0)
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
            if (game.human.PlayerWins.Count - game.pc.PlayerWins.Count > 6)
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
            else if (game.pc.PlayerWins.Count >= 1 && !alarm)
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
            else if (game.pc.PlayerWins.Count >= 1 && alarm)
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
        CellButtonModel chosenButton = game.boardModel.CellList[Service.RandomInt(game.boardModel.CellList.Count)];
        OnGenerateFinished(chosenButton);
    }

    private void FillCenterStrategy()
    {
        Debug.Log("FillCenterStrategy");
        decimal i = game.boardModel.BoardSettings.rowNumber / 2;
        int centerIndex = (int)System.Math.Round(i);
        int centerChar = Service.Alphabet[centerIndex];
        foreach(var c in game.boardModel.CellList)
        {
            Debug.Log(c.CellInt.ToString() + c.CellChar.ToString());
        }
        CellButtonModel chosenButton = game.boardModel.CellList.Single(c => c.CellInt.Equals(centerIndex) && c.CellChar.Equals(centerChar) && !c.Taken);
        if (chosenButton)
        {
            OnGenerateFinished(chosenButton);
        }
        else
        {
            FillDiagonalStrategy();
        }
    }

    private void FillDiagonalStrategy()
    {
        Debug.Log("FillDiagonalStrategy");
        List<CellButtonModel> availableDiagonals = GetAvailableDiagonals();
        if (availableDiagonals.Any())
        {
            CellButtonModel chosenButton = availableDiagonals[Service.RandomInt(availableDiagonals.Count)];
            OnGenerateFinished(chosenButton);
        }
        else
        {
            RandomStrategy();
        }
    }

    private void WinStrategy()
    {
        Debug.Log("WinStrategy");
        List<List<CellButtonModel>> actualWins = SortedWins(game.pc.PlayerWins);
        CellButtonModel chosenButton = actualWins[0].First(c => !c.Taken);
        OnGenerateFinished(chosenButton);
    }

    private void FailHumanStrategy()
    {
        Debug.Log("FailHumanStrategy");
        List<List<CellButtonModel>> humanWins = SortedWins(game.human.PlayerWins);
        CellButtonModel chosenButton = humanWins[0].Single(c => !c.Taken);
        OnGenerateFinished(chosenButton);
        alarm = false;
    }
    private List<CellButtonModel> GetAvailableDiagonals()
    {
        List<CellButtonModel> diagonals = new List<CellButtonModel>();
        int rowNumber = game.boardModel.BoardSettings.rowNumber;

        foreach (CellButtonModel cell in game.boardModel.CellList.FindAll(c => !c.Taken))
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

    private List<List<CellButtonModel>> SortedWins(List<List<CellButtonModel>> receivedWins)
    {
        WinsComparer wc = new WinsComparer();
        List<List<CellButtonModel>> actualWins = receivedWins;
        
        actualWins.Sort(wc);
        foreach (var i in actualWins[0]) Debug.Log(i.CellChar.ToString() + i.CellInt.ToString());
        return actualWins;
    }
    private void DetectAlarm()
    {
        List<List<CellButtonModel>> humanWins = SortedWins(game.human.PlayerWins);

        if (humanWins[0].FindAll(c => !c.Taken).Count == 1)
        {
            alarm = true;
        }
        foreach (var i in (humanWins[0])) Debug.Log(i.CellChar.ToString() + i.CellInt.ToString());
        Debug.Log(alarm);
    }
}

class WinsComparer : IComparer<List<CellButtonModel>>
{
    public int Compare(List<CellButtonModel> list1, List<CellButtonModel> list2)
    {
        int count1 = list1.FindAll(c => !c.Taken).Count;
        int count2 = list2.FindAll(c => !c.Taken).Count;

        if (count1 > count2)
        {
            return 1;
        }
        else if (count1 < count2)
        {
            return -1;
        }
        return 0;
    }
}