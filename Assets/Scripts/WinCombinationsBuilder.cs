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
    public abstract List<List<CellButtonModel>> GetResult();
}

class WinCombinationsBuilder : Builder
{
    List<List<CellButtonModel>> combination = new List<List<CellButtonModel>>();
    public override void BuildDiagonal1()
    {
        List<CellButtonModel> diagonal1 = new List<CellButtonModel>();
        

        for (int i = 0; i < game.boardModel.BoardSettings.rowNumber; i++)
        {
            foreach (CellButtonModel cell in game.boardModel.CellList)
            {
                if (cell.CellChar.Equals(Service.Alphabet[i]) && cell.CellInt.Equals(i))
                {
                    diagonal1.Add(cell);
                }
            }     
        }
        combination.Add(diagonal1);
    }
    public override void BuildDiagonal2()
    {
        List<CellButtonModel> diagonal2 = new List<CellButtonModel>();

        for (int i = 0; i < game.boardModel.BoardSettings.rowNumber; i++)
        {
            foreach (CellButtonModel cell in game.boardModel.CellList)
            {
                if (cell.CellChar.Equals(Service.Alphabet[game.boardModel.BoardSettings.rowNumber - i - 1]) && cell.CellInt.Equals(i))
                {
                    diagonal2.Add(cell);
                }
            }
        }
        combination.Add(diagonal2);
    }
    public override void BuildRows()
    {
        for (int i = 0; i < game.boardModel.BoardSettings.rowNumber; i++)
        {
            combination.Add(game.boardModel.CellList.FindAll(c => c.CellChar == Service.Alphabet[i]));
        }
    }
    public override void BuildColumns()
    {
        for (int i = 0; i < game.boardModel.BoardSettings.rowNumber; i++)
        {
            combination.Add(game.boardModel.CellList.FindAll(c => c.CellInt.Equals(i)));
        }
    }
    public override List<List<CellButtonModel>> GetResult()
    {
        return combination;
    }
}