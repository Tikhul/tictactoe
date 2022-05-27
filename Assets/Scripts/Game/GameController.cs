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
            Game.TicTacToeModel.GameModel.FinishedGame = true;
        }
    }

    public void GetResults(string result)
    {
        Debug.Log("GetResults");
        Game.TicTacToeView.FinalUI.ActivateResults(result);
        Game.TicTacToeController.BoardController.ManageButtons(false);
    }
}
