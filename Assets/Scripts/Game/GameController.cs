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
            Game.GameModel.FinishedGame = true;
        }
    }

    public void GetResult(string result)
    {
        Debug.Log("GetResult");
        Game.FinalUI.ActivateResults(result);
        Game.BoardController.ManageButtons(false);
    }
}
