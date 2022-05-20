using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellButtonModel : TicTacToeElement
// Кнопка для поля
{
    private int _cellInt;
    private char _cellChar;
    private bool _taken;
    public int CellInt
    {
        get => _cellInt;
        set => _cellInt = value;
    }
    public char CellChar
    {
        get => _cellChar;
        set => _cellChar = value;
    }
    public bool Taken
    {
        get => _taken;
        set => _taken = value;
    }
}
