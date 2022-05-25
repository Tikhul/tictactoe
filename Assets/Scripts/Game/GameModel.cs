using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : TicTacToeElement
{
    private bool _finishedGame = false;

    public bool FinishedGame
    {
        get => _finishedGame;
        set => _finishedGame = value;
    }
}
