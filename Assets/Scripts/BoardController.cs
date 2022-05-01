using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : TicTacToeElement
{
    public void CreateBoard()
     // Создания борда для игры
    {
        GameObject parentPanel = game.boardModel.BoardSettings.parentPanel;
        int rowNumber = game.boardModel.BoardSettings.rowNumber;
        float parentPanelSide = parentPanel.GetComponent<RectTransform>().sizeDelta.x;
        float buttonWidth = parentPanelSide / rowNumber;

        GameObject boardPanel = CreateBoardElement(parentPanel, game.boardModel.BoardParent,
            parentPanelSide, parentPanelSide);

        GameObject row = CreateBoardElement(game.boardModel.BoardSettings.rows, 
            boardPanel, parentPanelSide, parentPanelSide);

        for (int r = 0; r < rowNumber; r++)
        {
            GameObject column = CreateBoardElement(game.boardModel.BoardSettings.columns, row, buttonWidth, parentPanelSide);

            for (int b = 0; b < rowNumber; b++)
            {
                GameObject button = CreateBoardElement(game.boardModel.BoardSettings.buttonExample, column, buttonWidth, buttonWidth);
                FillCellist(button, b, r);
            }
        }
        CreateWinCombinations();
    }

    private void CreateWinCombinations()
    // Создание общих выирышных комбинаций в игре
    {
        List<CellButton> diagonal1 = new List<CellButton>();
        List<CellButton> diagonal2 = new List<CellButton>();
        List<CellButton> filterInt = new List<CellButton>();
        List<CellButton> filterChar = new List<CellButton>();
        int rownumber = game.boardModel.BoardSettings.rowNumber;

        for (int i = 0; i < rownumber; i++)
        {
            foreach(CellButton cell in game.boardModel.CellList)
            {
                if (cell.CellChar.Equals(Service.Alphabet[i]) && cell.CellInt.Equals(i))
                {
                    diagonal1.Add(cell);
                }
                    
                if (cell.CellChar.Equals(Service.Alphabet[rownumber - i - 1]) && cell.CellInt.Equals(i))
                {
                    diagonal2.Add(cell);
                }       
            }

            game.boardModel.WinCombinations.Add(game.boardModel.CellList.FindAll(c => c.CellInt.Equals(i)));
            game.boardModel.WinCombinations.Add(game.boardModel.CellList.FindAll(c => c.CellChar == Service.Alphabet[i]));
        }

        game.boardModel.WinCombinations.Add(diagonal1);
        game.boardModel.WinCombinations.Add(diagonal2);
    }

    GameObject CreateBoardElement(GameObject objToCreate, GameObject parent, float width, float height)
    {
        GameObject newObject = Instantiate(objToCreate);
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        newObject.transform.SetParent(parent.transform);
        newObject.transform.localScale = new Vector3(1, 1, 1);
        newObject.transform.localPosition = newObject.transform.position;
        return newObject;
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

    public void DestroyBoard()
    {

    }
}
