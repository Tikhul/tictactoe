using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject boardParent;
    public BoardScriptableObject boardSettings;
    public List<string> winCombinations;
    public List<CellButton> cellList;
    private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public void CreateBoard()
    {
        CanvasRenderer boardPanel = CreateBoardPanel();
        HorizontalLayoutGroup row = CreateRow(boardPanel);

        float canvasWidth = boardPanel.GetComponent<RectTransform>().sizeDelta.x;
        float buttonWidth = canvasWidth / boardSettings.rowNumber;

        for (int r = 0; r < boardSettings.rowNumber; r++)
        {
            VerticalLayoutGroup column = CreateColumn(row, buttonWidth, canvasWidth);
            winCombinations.Add(r+alphabet.Substring(0, boardSettings.rowNumber));
  
            for (int b = 0; b < boardSettings.rowNumber; b++)
            {
                Button button = CreateButton(column, buttonWidth, buttonWidth);
                CellButton buttonSettings = button.GetComponent<CellButton>();
                buttonSettings.cellChar = alphabet[b];
                buttonSettings.cellInt = r;
                cellList.Add(buttonSettings);
                if (r == boardSettings.rowNumber - 1)
                {
                    List<int> tempList = Enumerable.Range(0, boardSettings.rowNumber).ToList();
                    string s = string.Join("", tempList);
                    winCombinations.Add(alphabet[b] + s);
                }
            }
        }
        CreateWinCombinations();
    }

    CanvasRenderer CreateBoardPanel()
    {
        CanvasRenderer boardPanel = Instantiate(boardSettings.parentPanel);
        boardPanel.transform.SetParent(boardParent.transform);
        boardPanel.transform.localScale = new Vector3(1, 1, 1);
        boardPanel.transform.localPosition = boardSettings.parentPanel.transform.position;
        return boardPanel;
    }

    HorizontalLayoutGroup CreateRow(CanvasRenderer boardPanel)
    {
        HorizontalLayoutGroup row = Instantiate(boardSettings.rows);
        row.transform.SetParent(boardPanel.transform);
        row.transform.localPosition = boardSettings.rows.transform.position;
        row.transform.localScale = new Vector3(1, 1, 1);
        return row;
    }

    VerticalLayoutGroup CreateColumn(HorizontalLayoutGroup row, float width, float height)
    {
        VerticalLayoutGroup column = Instantiate(boardSettings.columns);
        column.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        column.transform.SetParent(row.transform);
        column.transform.localPosition = boardSettings.columns.transform.position;
        column.transform.localScale = new Vector3(1, 1, 1);
        return column;
    }

    Button CreateButton(VerticalLayoutGroup column, float width, float height)
    {
        Button button = Instantiate(boardSettings.buttonExample);
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        button.transform.SetParent(column.transform);
        button.transform.localPosition = boardSettings.buttonExample.transform.position;
        button.transform.localScale = new Vector3(1, 1, 1);
        return button;
    }
    void CreateWinCombinations()
    {
        string diagonal1 = "";
        string diagonal2 = "";
        for (int i = 0; i < boardSettings.rowNumber; i++)
        {
            diagonal1 += i + alphabet[i].ToString();
            diagonal2 += i+alphabet[boardSettings.rowNumber - i - 1].ToString();
        }

        winCombinations.Add(diagonal1);
        winCombinations.Add(diagonal2);

    }

    public List<string> CheckWinCombinations(List<string> wins, int cellInt, char cellChar)
    {
        foreach (var win in wins) Debug.Log("Input wins " + win);
        List<string> tempList = new List<string>();

        foreach (var win in wins)
        {
            if (win.Contains(cellInt.ToString()) && win.Contains(cellChar.ToString())) tempList.Add(win);
        }

        wins.RemoveAll(item => tempList.Contains(item));
        foreach (var win in wins) Debug.Log("Output wins " + win);
        return wins;
    }

    public void CellsAfterTurn(int cellInt, char cellChar)
    {
        List<CellButton> tempList = new List<CellButton>();

        foreach (var cell in cellList)
           {
               if (cell.cellInt.Equals(cellInt) && cell.cellChar.Equals(cellChar)) tempList.Add(cell);
           }

        cellList.RemoveAll(item => tempList.Contains(item));
    }
}
