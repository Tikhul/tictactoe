using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class GameStateController : TicTacToeElement

//    // Общее состояние игры
//{
//    public bool finishedGame;

//    //private void OnEnable()
//    //{
//    //    CreatePlayersButton.OnPlayerChosen += GameStarted;
//    //}

//    //private void OnDisable()
//    //{
//    //    CreatePlayersButton.OnPlayerChosen -= GameStarted;
//    //}
//    //void GameStarted(string actualMarker)
//    //{
//    //    finishedGame = false;
//    //    game.boardController.CreateBoard();
//    //    game.playerController.CreatePlayers(actualMarker);
//    //    game.stepExecutionController.LaunchFirstTurn(actualMarker);
//    //    CreatePlayersButton.OnPlayerChosen -= GameStarted;
//    //}

//    public void CheckGameState(PlayerModel player, CellButton cell)
//    {
//        game.boardController.CellsAfterTurn(cell);
//        game.playerController.UpdatePlayers(player, cell);
//        game.playerController.CheckRemainingWins();
//        game.playerController.LaunchWinnerDetection(player);
//    }
//}
