using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : TicTacToeElement
{
    private void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += GameStarted;
    }

    private void OnDisable()
    {
        
    }
    void GameStarted(string actualMarker)
    {
        game.boardController.CreateBoard(actualMarker);
        game.playerController.CreatePlayers(actualMarker);
        game.stepExecutionController.LaunchFirstTurn(actualMarker);
        CreatePlayersButton.OnPlayerChosen -= GameStarted;
    }

    public void CheckGameState(string actualMarker, CellButton cell)
    {
        game.boardController.CellsAfterTurn(cell);
        game.playerController.UpdatePlayers(actualMarker, cell);
        game.playerController.LaunchWinnerDetection(actualMarker);
    }

    public void FinishGame()
    {
        Debug.Log("Finish game");
        game.stepExecutionController.OutOfTurns();
    }

}
