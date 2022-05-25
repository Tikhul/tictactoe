//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class StepExecutionController : TicTacToeElement
//{
//    // Выполнение шагов человека и ПК, запуск проверок после шага
//    private void OnEnable()
//    {
//        //CellButton.OnPlayerClick += GetPlayerTurn;
//        //PCController.OnGenerateFinished += GetPCTurn;
//    }

//    private void GetPCTurn(CellButton cell)
//    {
//        TurnExecuted(game.pc, cell);
//        if(!game.gameStateController.finishedGame) Service.ActivateButtons();
//    }

//    //private void GetPlayerTurn(CellButton cell)
//    //{
//    //    TurnExecuted(game.human, cell);
//    //    //if (!game.gameStateController.finishedGame) LaunchPCTurn();
//    //}

//    private void TurnExecuted(PlayerModel player, CellButton cell)
//    {
//       // game.gameStateController.CheckGameState(player, cell);
//    }

//    public void OutOfTurns(string result)
//    {
//        //PCController.OnGenerateFinished -= GetPCTurn;
//        //CellButton.OnPlayerClick -= GetPlayerTurn;
//        game.finalUI.ActivateResults(result);
//        Service.BlockButtons();
//    }
//}
