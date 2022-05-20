using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : TicTacToeElement
{
    private bool _isHuman;
    private string _marker;
    private bool _isWinner = false;
    public static string MarkerX = "X";
    public static string MarkerZero = "0";
    private List<List<CellButtonModel>> _actualWinStrategy = new List<List<CellButtonModel>>();
    private List<List<CellButtonModel>> _playerWins = new List<List<CellButtonModel>>();
    private List<CellButtonModel> _playerTurns = new List<CellButtonModel>();
    public string Marker 
    { 
        get
        {
            if (_marker.Equals(MarkerZero) || _marker.Equals(MarkerX))
            {
                return _marker;
            }
            else
            {
                Debug.Log("Проверьте маркеры в кнопке создания игроков");
                return null;
            }
        }
        set => _marker = value;
    }

    public bool IsHuman 
    { 
        get => _isHuman; 
        set => _isHuman = value; 
    }
    public bool IsWinner 
    { 
        get => _isWinner;
        set { if (value == true) _isWinner = value; } 
    }
    public List<List<CellButtonModel>> ActualWinStrategy
    {
        get => _actualWinStrategy;
        set => _actualWinStrategy = value;
    }
    public List<List<CellButtonModel>> PlayerWins
    {
        get => _playerWins;
        set => _playerWins = value;
    }
    public List<CellButtonModel> PlayerTurns
    {
        get => _playerTurns;
        set => _playerTurns = value;
    }
}
