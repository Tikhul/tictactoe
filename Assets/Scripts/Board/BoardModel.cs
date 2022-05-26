﻿using System.Collections.Generic;
using UnityEngine;

public class BoardModel : TicTacToeElement
{
    public static string Alphabet = "abcdefghijklmnopqrstuvwxyz";
    private List<CellButton> _cellList = new List<CellButton>();
    private List<List<CellButton>> _winCombinations = new List<List<CellButton>>();
    [SerializeField] private GameObject _boardParent;
    [SerializeField] private BoardScriptableObject _boardSettings;
    public List<CellButton> CellList 
    {   
        get => _cellList; 
        set => _cellList = value; 
    }
    public List<List<CellButton>> WinCombinations 
    { 
        get => _winCombinations; 
        set => _winCombinations = value; 
    }
    public GameObject BoardParent
    {
        get => _boardParent;
        set => _boardParent = value;
    }
    public BoardScriptableObject BoardSettings
    {
        get
        {
            if (_boardSettings.rowNumber > 2 && _boardSettings.rowNumber < 27)
            {
                return _boardSettings;
            }
            else
            {
                Debug.Log("Выберите количество клеток от 3 до 26");
                return null;
            }
        }
        set => _boardSettings = value;


    }
}
