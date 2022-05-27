using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Director
{
    private Builder builder;

    public Director(Builder builder)
    {
        this.builder = builder;
    }
    public void Construct()
    {
        builder.BuildDiagonal1();
        builder.BuildDiagonal2();
        builder.BuildRows();
        builder.BuildColumns();
    }
}

abstract class Builder : TicTacToeElement
{
    public abstract void BuildDiagonal1();
    public abstract void BuildDiagonal2();
    public abstract void BuildRows();
    public abstract void BuildColumns();
    public abstract List<List<CellButton>> GetResult();
}

class WinCombinationsBuilder : Builder
{
    List<List<CellButton>> combination = new List<List<CellButton>>();
    public override void BuildDiagonal1()
    {
        List<CellButton> diagonal1 = new List<CellButton>();
        

        for (int i = 0; i < Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber; i++)
        {
            foreach (CellButton cell in Game.TicTacToeModel.BoardModel.CurrentCellList)
            {
                if (cell.CellChar.Equals(BoardModel.Alphabet[i]) && cell.CellInt.Equals(i))
                {
                    diagonal1.Add(cell);
                }
            }     
        }
        combination.Add(diagonal1);
    }
    public override void BuildDiagonal2()
    {
        List<CellButton> diagonal2 = new List<CellButton>();

        for (int i = 0; i < Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber; i++)
        {
            foreach (CellButton cell in Game.TicTacToeModel.BoardModel.CurrentCellList)
            {
                if (cell.CellChar.Equals(BoardModel.Alphabet[Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber - i - 1]) && cell.CellInt.Equals(i))
                {
                    diagonal2.Add(cell);
                }
            }
        }
        combination.Add(diagonal2);
    }
    public override void BuildRows()
    {
        for (int i = 0; i < Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber; i++)
        {
            combination.Add(Game.TicTacToeModel.BoardModel.CurrentCellList.FindAll(c => c.CellChar == BoardModel.Alphabet[i]));
        }
    }
    public override void BuildColumns()
    {
        for (int i = 0; i < Game.TicTacToeModel.BoardModel.BoardSettings.rowNumber; i++)
        {
            combination.Add(Game.TicTacToeModel.BoardModel.CurrentCellList.FindAll(c => c.CellInt.Equals(i)));
        }
    }
    public override List<List<CellButton>> GetResult()
    {
        return combination;
    }
}