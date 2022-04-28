using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : TicTacToeElement
{
    public void CreateBoard(string actualMarker)
     // Создания борда для игры
    {
        GameObject parentPanel = game.boardModel.boardSettings.parentPanel;
        int rowNumber = game.boardModel.boardSettings.rowNumber;
        float parentPanelX = parentPanel.GetComponent<RectTransform>().sizeDelta.x;
        float parentPanelY = parentPanel.GetComponent<RectTransform>().sizeDelta.y;
        float buttonWidth = parentPanelX / rowNumber;

        GameObject boardPanel = CreateBoardElement(parentPanel, game.boardModel.boardParent,
            parentPanelX, parentPanelY);

        GameObject row = CreateBoardElement(game.boardModel.boardSettings.rows, 
            boardPanel, parentPanelX, parentPanelY);

        for (int r = 0; r < rowNumber; r++)
        {
            GameObject column = CreateBoardElement(game.boardModel.boardSettings.columns, row, buttonWidth, parentPanelX);

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
        string diagonal1 = "";
        string diagonal2 = "";
        List<string> filterInt = new List<string>();
        List<string> filterChar = new List<string>();

        for (int i = 0; i < game.boardModel.boardSettings.rowNumber; i++)
        {
            diagonal1 += game.boardModel.alphabet[i].ToString() + i;
            diagonal2 += game.boardModel.alphabet[game.boardModel.boardSettings.rowNumber - i - 1].ToString() + i;

            string forInt = "";

            foreach (var e in game.boardModel.cellList.Where(c => c.cellInt == i))
                forInt += e.cellChar.ToString() + e.cellInt.ToString();

            filterInt.Add(forInt);

            string forChar = "";

            foreach (var e in game.boardModel.cellList.FindAll(c => c.cellChar == game.boardModel.alphabet[i]))
                forChar += e.cellChar.ToString() + e.cellInt.ToString();

            filterChar.Add(forChar);
        }

        game.boardModel.winCombinations.Add(diagonal1);
        game.boardModel.winCombinations.Add(diagonal2);
        game.boardModel.winCombinations.AddRange(filterInt);
        game.boardModel.winCombinations.AddRange(filterChar);

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
                if (cell.cellInt.Equals(receivedCell.cellInt) && cell.cellChar.Equals(receivedCell.cellChar))
                    tempList.Add(cell);
            }

            game.boardModel.cellList.RemoveAll(item => tempList.Contains(item));
        }
        else
        {
            game.gameStateController.FinishGame();
            game.finalUI.ActivateResults("Ничья");
        }    
    }

    public void DestroyBoard()
    {

    }
}
