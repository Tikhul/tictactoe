﻿using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.Specialized;

public class Board : MonoBehaviour
{
    public GameObject boardParent;
    public BoardScriptableObject boardSettings;
    public List<string> winCombinations;
    private const string alphabet = "abcdefghijklmnopqrstuvwxyz";

    void OnEnable()
    {
       // CreatePlayersButton.OnPlayerChosen += CreateBoard;
    }

    void OnDisable()
    {
       // CreatePlayersButton.OnPlayerChosen -= CreateBoard;
    }

    private void Start()
    {
        CreateBoard();
    }

    void CreateBoard()
    {
        CanvasRenderer boardPanel = CreateBoardPanel();
        HorizontalLayoutGroup column = CreateColumn(boardPanel);

        float canvasWidth = boardPanel.GetComponent<RectTransform>().sizeDelta.x;
        float buttonWidth = canvasWidth / boardSettings.rowNumber;

        for (int r = 0; r < boardSettings.rowNumber; r++)
        {
            VerticalLayoutGroup row = CreateRow(column, buttonWidth, canvasWidth);
            winCombinations.Add(r+alphabet.Substring(0, boardSettings.rowNumber));
  
            for (int b = 0; b < boardSettings.rowNumber; b++)
            {
                Button button = CreateButton(row, buttonWidth, buttonWidth);
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

    HorizontalLayoutGroup CreateColumn(CanvasRenderer boardPanel)
    {
        HorizontalLayoutGroup column = Instantiate(boardSettings.columns);
        column.transform.SetParent(boardPanel.transform);
        column.transform.localPosition = boardSettings.columns.transform.position;
        column.transform.localScale = new Vector3(1, 1, 1);
        return column;
    }

    VerticalLayoutGroup CreateRow(HorizontalLayoutGroup column, float width, float height)
    {
        VerticalLayoutGroup row = Instantiate(boardSettings.rows);
        row.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        row.transform.SetParent(column.transform);
        row.transform.localPosition = boardSettings.rows.transform.position;
        row.transform.localScale = new Vector3(1, 1, 1);
        return row;
    }

    Button CreateButton(VerticalLayoutGroup row, float width, float height)
    {
        Button button = Instantiate(boardSettings.buttonExample);
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        button.transform.SetParent(row.transform);
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
            diagonal2 += i+alphabet[boardSettings.rowNumber - i].ToString();
        }

        winCombinations.Add(diagonal1);
        winCombinations.Add(diagonal2);

        foreach (var i in winCombinations) Debug.Log(i);

    }

    void CheckWinCombinations()
    {
        
    }
}