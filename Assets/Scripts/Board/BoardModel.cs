using System.Collections.Generic;
using UnityEngine;

public class BoardModel : TicTacToeElement
{
    private List<CellButtonModel> _cellList = new List<CellButtonModel>();
    private List<List<CellButtonModel>> _winCombinations = new List<List<CellButtonModel>>();
    [SerializeField] private GameObject _boardParent;
    [SerializeField] private BoardScriptableObject _boardSettings;
    public List<CellButtonModel> CellList 
    {   
        get => _cellList; 
        set => _cellList = value; 
    }
    public List<List<CellButtonModel>> WinCombinations 
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
