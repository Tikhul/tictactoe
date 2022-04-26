using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BoardController : TicTacToeElement
{
    private void OnEnable()
    {
        BoardView.OnGameStarted += CreateBoard;
        CellButton.OnPlayerClick += CellsAfterTurn;
        CellButton.OnPCTaken += CellsAfterTurn;
    }

    private void OnDisable()
    {
        
        CellButton.OnPlayerClick -= CellsAfterTurn;
        CellButton.OnPCTaken -= CellsAfterTurn;
    }
    void CreateBoard()
    {
        
        CanvasRenderer boardPanel = CreateBoardPanel();
        HorizontalLayoutGroup row = CreateRow(boardPanel);

        float canvasWidth = boardPanel.GetComponent<RectTransform>().sizeDelta.x;
        float buttonWidth = canvasWidth / game.boardModel.boardSettings.rowNumber;

        for (int r = 0; r < game.boardModel.boardSettings.rowNumber; r++)
        {
            VerticalLayoutGroup column = CreateColumn(row, buttonWidth, canvasWidth);
            game.boardModel.winCombinations.Add(r + game.boardModel.alphabet.Substring(0, game.boardModel.boardSettings.rowNumber));

            for (int b = 0; b < game.boardModel.boardSettings.rowNumber; b++)
            {
                Button button = CreateButton(column, buttonWidth, buttonWidth);
                CellButton buttonSettings = button.GetComponent<CellButton>();
                buttonSettings.cellChar = game.boardModel.alphabet[b];
                buttonSettings.cellInt = r;
                game.boardModel.cellList.Add(buttonSettings);
                if (r == game.boardModel.boardSettings.rowNumber - 1)
                {
                    List<int> tempList = Enumerable.Range(0, game.boardModel.boardSettings.rowNumber).ToList();
                    string s = string.Join("", tempList);
                    game.boardModel.winCombinations.Add(game.boardModel.alphabet[b] + s);
                }
            }
        }
        BoardView.OnGameStarted -= CreateBoard;
        CreateWinCombinations();
    }

    CanvasRenderer CreateBoardPanel()
    {
        CanvasRenderer boardPanel = Instantiate(game.boardModel.boardSettings.parentPanel);
        boardPanel.transform.SetParent(game.boardModel.boardParent.transform);
        boardPanel.transform.localScale = new Vector3(1, 1, 1);
        boardPanel.transform.localPosition = game.boardModel.boardSettings.parentPanel.transform.position;
        return boardPanel;
    }

    HorizontalLayoutGroup CreateRow(CanvasRenderer boardPanel)
    {
        HorizontalLayoutGroup row = Instantiate(game.boardModel.boardSettings.rows);
        row.transform.SetParent(boardPanel.transform);
        row.transform.localPosition = game.boardModel.boardSettings.rows.transform.position;
        row.transform.localScale = new Vector3(1, 1, 1);
        return row;
    }

    VerticalLayoutGroup CreateColumn(HorizontalLayoutGroup row, float width, float height)
    {
        VerticalLayoutGroup column = Instantiate(game.boardModel.boardSettings.columns);
        column.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        column.transform.SetParent(row.transform);
        column.transform.localPosition = game.boardModel.boardSettings.columns.transform.position;
        column.transform.localScale = new Vector3(1, 1, 1);
        return column;
    }

    Button CreateButton(VerticalLayoutGroup column, float width, float height)
    {
        Button button = Instantiate(game.boardModel.boardSettings.buttonExample);
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        button.transform.SetParent(column.transform);
        button.transform.localPosition = game.boardModel.boardSettings.buttonExample.transform.position;
        button.transform.localScale = new Vector3(1, 1, 1);
        return button;
    }
    void CreateWinCombinations()
    {
        string diagonal1 = "";
        string diagonal2 = "";
        for (int i = 0; i < game.boardModel.boardSettings.rowNumber; i++)
        {
            diagonal1 += i + game.boardModel.alphabet[i].ToString();
            diagonal2 += i + game.boardModel.alphabet[game.boardModel.boardSettings.rowNumber - i - 1].ToString();
        }

        game.boardModel.winCombinations.Add(diagonal1);
        game.boardModel.winCombinations.Add(diagonal2);
    }

    public List<string> CheckWinCombinations(List<string> wins, int cellInt, char cellChar)
    {
        List<string> tempList = new List<string>();

        foreach (var win in wins)
        {
            if (win.Contains(cellInt.ToString()) && win.Contains(cellChar.ToString())) tempList.Add(win);
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
}
