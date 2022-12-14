using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : TicTacToeElement
{
    private bool _isHuman;
    private string _marker;
    private bool _isWinner = false;
    private List<List<CellButton>> _actualWinStrategy = new List<List<CellButton>>();
    private List<List<CellButton>> _playerWins = new List<List<CellButton>>();
    private List<CellButton> _playerTurns = new List<CellButton>();
    public string Marker 
    { 
        get
        {
            if (_marker.Equals(PlayerMarker.X.ToString()) || _marker.Equals(PlayerMarker.O.ToString()))
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
    public enum PlayerMarker
    {
        X,
        O
    }
}
