using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : TicTacToeElement
{
    public void CreateBoard()
     // Создания борда для игры
    {
        GameObject parentPanel = game.boardModel.boardSettings.parentPanel;
        int rowNumber = game.boardModel.boardSettings.rowNumber;
        float parentPanelSide = parentPanel.GetComponent<RectTransform>().sizeDelta.x;
        float buttonWidth = parentPanelSide / rowNumber;

        GameObject boardPanel = CreateBoardElement(parentPanel, game.boardModel.boardParent,
            parentPanelSide, parentPanelSide);

        GameObject row = CreateBoardElement(game.boardModel.boardSettings.rows, 
            boardPanel, parentPanelSide, parentPanelSide);

        for (int r = 0; r < rowNumber; r++)
        {
            GameObject column = CreateBoardElement(game.boardModel.boardSettings.columns, row, buttonWidth, parentPanelSide);

            for (int b = 0; b < rowNumber; b++)
            {
                GameObject button = CreateBoardElement(game.boardModel.boardSettings.buttonExample, column, buttonWidth, buttonWidth);
                FillCellist(button, b, r);
            }
        }
        CreateWinCombinations();
    }

    void CreateWinCombinations()
    // Создание общих выирышных комбинаций в игре
    {
        List<CellButton> diagonal1 = new List<CellButton>();
        List<CellButton> diagonal2 = new List<CellButton>();
        List<CellButton> filterInt = new List<CellButton>();
        List<CellButton> filterChar = new List<CellButton>();
        string alphabet = game.boardModel.alphabet;
        int rownumber = game.boardModel.boardSettings.rowNumber;

        for (int i = 0; i < rownumber; i++)
        {
            foreach(CellButton cell in game.boardModel.cellList)
            {
                if (cell.cellChar.Equals(alphabet[i]) && cell.cellInt.Equals(i))
                    diagonal1.Add(cell);
                if (cell.cellChar.Equals(alphabet[rownumber - i - 1]) && cell.cellInt.Equals(i))
                    diagonal2.Add(cell);
            }

            game.boardModel.winCombinations.Add(game.boardModel.cellList.FindAll(c => c.cellInt.Equals(i)));
            game.boardModel.winCombinations.Add(game.boardModel.cellList.FindAll(c => c.cellChar == alphabet[i]));
        }

        game.boardModel.winCombinations.Add(diagonal1);
        game.boardModel.winCombinations.Add(diagonal2);
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

    void FillCellist(GameObject button, int b, int r)
    // Заполняю лист актуальными ячейками
    {
        CellButton buttonSettings = button.GetComponent<CellButton>();
        buttonSettings.cellChar = game.boardModel.alphabet[b];
        buttonSettings.cellInt = r;
        game.boardModel.cellList.Add(buttonSettings);
    }
    
    public void CellsAfterTurn(CellButton receivedCell)
    // Проверка оставшихся ячеек после каждого хода
    {
        if (game.boardModel.cellList.Any())
        {
            List<CellButton> tempList = new List<CellButton>();

            foreach (var cell in game.boardModel.cellList)
            {
                if (cell.Equals(receivedCell)) tempList.Add(cell);
            }

            game.boardModel.cellList.RemoveAll(item => tempList.Contains(item));
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
