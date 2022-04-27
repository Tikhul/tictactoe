using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            game.boardModel.winCombinations.Add(r + game.boardModel.alphabet.Substring(0, rowNumber));

            for (int b = 0; b < rowNumber; b++)
            {
                GameObject button = CreateBoardElement(game.boardModel.boardSettings.buttonExample, column, buttonWidth, buttonWidth);
                FillCellist(button, b, r);

                if (r == rowNumber - 1)
                {
                    CreateDiagonalWinCombinations(b);
                }
            }
        }
        CreateMainWinCombinations();
        CreatePlayersButton.OnPlayerChosen -= CreateBoard;
        OnBoardCreated?.Invoke(actualMarker);
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

    void CreateMainWinCombinations()
    {
        string diagonal1 = "";
        string diagonal2 = "";
        for (int i = 0; i < game.boardModel.boardSettings.rowNumber; i++)
        {
            diagonal1 += game.boardModel.alphabet[i].ToString() + i;
            diagonal2 += game.boardModel.alphabet[game.boardModel.boardSettings.rowNumber - i - 1].ToString() + i;
        }

        game.boardModel.winCombinations.Add(diagonal1);
        game.boardModel.winCombinations.Add(diagonal2);
    }

    void CreateDiagonalWinCombinations(int b)
    {
        List<int> tempList = Enumerable.Range(0, game.boardModel.boardSettings.rowNumber).ToList();
        string s = string.Join("", tempList);
        game.boardModel.winCombinations.Add(game.boardModel.alphabet[b] + s);
    }
    void FillCellist(GameObject button, int b, int r)
    {
        CellButton buttonSettings = button.GetComponent<CellButton>();
        buttonSettings.cellChar = game.boardModel.alphabet[b];
        buttonSettings.cellInt = r;
        game.boardModel.cellList.Add(buttonSettings);
    }
    public List<string> CheckWinCombinations(List<string> wins, int cellInt, char cellChar)
    {
        List<string> tempList = new List<string>();

        foreach (var win in wins)
        {
            for (int i=0; i< win.Length -1; i++)
            {
                if (win[i].ToString().Contains(cellInt.ToString()) && win[i+1].ToString().Contains(cellChar.ToString())) 
                    tempList.Add(win);
            }
        }

        wins.RemoveAll(item => tempList.Contains(item));

        return wins;
    }
    
    public void CellsAfterTurn(string actualMarker, int cellInt, char cellChar)
    {
        List<CellButton> tempList = new List<CellButton>();

        foreach (var cell in game.boardModel.cellList)
        {
            if (cell.cellInt.Equals(cellInt) && cell.cellChar.Equals(cellChar)) tempList.Add(cell);
        }

        game.boardModel.cellList.RemoveAll(item => tempList.Contains(item));
    }

    void DestroyBoard()
    {

    }
}
