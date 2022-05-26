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

    public void GetResults(string result)
    {
        Debug.Log("GetResults");
        Game.FinalUI.ActivateResults(result);
        Game.BoardController.ManageButtons(false);
    }
}
