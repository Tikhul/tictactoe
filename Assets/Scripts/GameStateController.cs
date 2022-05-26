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
//    //    Game.boardController.CreateBoard();
//    //    Game.playerController.CreatePlayers(actualMarker);
//    //    Game.stepExecutionController.LaunchFirstTurn(actualMarker);
//    //    CreatePlayersButton.OnPlayerChosen -= GameStarted;
//    //}

//    public void CheckGameState(PlayerModel player, CellButton cell)
//    {
//        Game.boardController.CellsAfterTurn(cell);
//        Game.playerController.UpdatePlayers(player, cell);
//        Game.playerController.CheckRemainingWins();
//        Game.playerController.LaunchWinnerDetection(player);
//    }
//}
