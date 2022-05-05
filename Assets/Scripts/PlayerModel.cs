using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : TicTacToeElement
{
    // public bool isHuman;
    private string _marker; private PlayerMarker marker = PlayerMarker.X;
    private bool _isWinner = false;
    //public static string MarkerX = "X";
    //public static string MarkerZero = "0";
    
    private List<List<CellButton>> _actualWinStrategy = new List<List<CellButton>>();
    private List<List<CellButton>> _playerWins = new List<List<CellButton>>();
    private List<CellButton> _playerTurns = new List<CellButton>();
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

    public bool IsHuman { get; set; }
    public bool IsWinner 
    { 
        get => _isWinner;
        set { if (value == true) _isWinner = value; } 
    }
    public List<List<CellButton>> ActualWinStrategy
    {
        get => _actualWinStrategy;
        set => _actualWinStrategy = value;
    }
    public List<List<CellButton>> PlayerWins
    {
        get => _playerWins;
        set => _playerWins = value;
    }
    public List<CellButton> PlayerTurns
    {
        get => _playerTurns;
        set => _playerTurns = value;
    }
}


public enum PlayerMarker
{
    X,
    O
}