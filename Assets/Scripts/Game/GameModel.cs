using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : TicTacToeElement
{
    private bool _finishedGame;

    public bool FinishedGame
    {
        get => _finishedGame = false;
        set => _finishedGame = value;
    }
}
