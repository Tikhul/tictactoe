using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : TicTacToeElement
{
    public void CreateBoard()
     // Создания борда для игры
    {
        BoardPartsPrototype prototype = new StandardPartPrototype();

        GameObject boardPanel = prototype.Clone(prototype.ParentPanel, prototype.BoardParent, 
            prototype.ParentPanelSide, prototype.ParentPanelSide);

        GameObject row = prototype.Clone(prototype.Rows, boardPanel, prototype.ParentPanelSide, prototype.ParentPanelSide);

        for (int r = 0; r < prototype.RowNumber; r++)
        {
            GameObject column = prototype.Clone(prototype.Columns, row, prototype.ButtonWidth, prototype.ParentPanelSide);

            for (int b = 0; b < prototype.RowNumber; b++)
            {
                GameObject button = prototype.Clone(prototype.ButtonExample, column, prototype.ButtonWidth, prototype.ButtonWidth);
                FillCellist(button, b, r);
            }
        }
        CreateWinCombinations();
    }

    private void CreateWinCombinations()
    // Создание общих выирышных комбинаций в игре
    {
        Builder builder = new WinCombinationsBuilder();
        Director director = new Director(builder);
        director.Construct();
        game.boardModel.WinCombinations.AddRange(builder.GetResult());
    }

    private void FillCellist(GameObject button, int buttonIndex, int rowIndex)
    // Заполняю лист актуальными ячейками
    {
        CellButton buttonSettings = button.GetComponent<CellButton>();
        buttonSettings.CellChar = Service.Alphabet[buttonIndex];
        buttonSettings.CellInt = rowIndex;
        game.boardModel.CellList.Add(buttonSettings);
    }
    
    public void CellsAfterTurn(CellButton receivedCell)
    // Проверка оставшихся ячеек после каждого хода
    {
        if (game.boardModel.CellList.Any())
        {
            List<CellButton> tempList = new List<CellButton>();

            foreach (var cell in game.boardModel.CellList)
            {
                if (cell.CellChar.Equals(receivedCell.CellChar) && cell.CellInt.Equals(receivedCell.CellInt)) 
                {
                    tempList.Add(cell);
                } 
            }
            game.boardModel.CellList.RemoveAll(item => tempList.Contains(item));
        }
        else
        {
            game.gameStateController.finishedGame = true;
            game.stepExecutionController.OutOfTurns("Ничья");
        }    
    }
}
