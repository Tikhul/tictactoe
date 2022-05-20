using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellButtonController : TicTacToeElement
{
    private void OnEnable()
    {
        CellButton.OnPlayerClick += ChangeCellStatus;
        CellButton.OnDataReceived += FillCellIndex;
    }

    private void OnDisable()
    {
        CellButton.OnPlayerClick -= ChangeCellStatus;
        CellButton.OnDataReceived -= FillCellIndex;
    }

    private void ChangeCellStatus(CellButtonModel cell)
    {
        cell.Taken = true;
    }

    public delegate void FillAction(CellButtonModel cell);
    public static event FillAction OnCellFilled;
    private void FillCellIndex(CellButtonModel cell, int buttonIndex, int rowIndex)
    {
        cell.CellChar = Service.Alphabet[buttonIndex];
        cell.CellInt = rowIndex;
        OnCellFilled(cell);
    }
}
