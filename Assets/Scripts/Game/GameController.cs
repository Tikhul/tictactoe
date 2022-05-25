using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : TicTacToeElement

// Общее состояние игры
{
    public void CheckGameState(bool finished)
    {
        if (finished)
        {
            game.gameModel.FinishedGame = true;
        }
    }
}
