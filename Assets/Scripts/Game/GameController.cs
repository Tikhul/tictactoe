using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : TicTacToeElement

// Общее состояние игры
{
    public void CheckGameState(bool state)
    {
       Game.TicTacToeModel.GameModel.FinishedGame = state;
    }

    public void GetResults(string result)
    {
        Debug.Log("GetResults");
        Game.TicTacToeView.FinalUI.ActivateResults(result);
        Game.TicTacToeController.BoardController.ManageButtons(false);
    }
}
