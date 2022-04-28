using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : TicTacToeElement
{
    private void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += CreateBoard;
        CellButton.OnPlayerClick += CellsAfterTurn;
        CellButton.OnPCTaken += CellsAfterTurn;
    }

    private void OnDisable()
    {
        CellButton.OnPlayerClick -= CellsAfterTurn;
        CellButton.OnPCTaken -= CellsAfterTurn;
    }

    public delegate void BoardAction(string marker);
    public static event BoardAction OnBoardCreated;
    void CreateBoard(string actualMarker)
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
        CreatePlayersButton.OnPlayerChosen -= CreateBoard;
        OnBoardCreated?.Invoke(actualMarker);
    }

    void CreateWinCombinations()
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
    {
        CellButton buttonSettings = button.GetComponent<CellButton>();
        buttonSettings.cellChar = game.boardModel.alphabet[b];
        buttonSettings.cellInt = r;
        game.boardModel.cellList.Add(buttonSettings);
    }
    
    public void CellsAfterTurn(string actualMarker, int cellInt, char cellChar)
    {
        List<CellButton> tempList = new List<CellButton>();

        foreach (var cell in game.boardModel.cellList)
        {
            if (cell.cellInt.Equals(cellInt) && cell.cellChar.Equals(cellChar)) 
                tempList.Add(cell);
        }

        game.boardModel.cellList.RemoveAll(item => tempList.Contains(item));
    }

    void DestroyBoard()
    {

    }
}
