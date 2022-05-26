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
//        TurnExecuted(Game.pc, cell);
//        if(!Game.gameStateController.finishedGame) Service.ActivateButtons();
//    }

//    //private void GetPlayerTurn(CellButton cell)
//    //{
//    //    TurnExecuted(Game.human, cell);
//    //    //if (!Game.gameStateController.finishedGame) LaunchPCTurn();
//    //}

//    private void TurnExecuted(PlayerModel player, CellButton cell)
//    {
//       // Game.gameStateController.CheckGameState(player, cell);
//    }

//    public void OutOfTurns(string result)
//    {
//        //PCController.OnGenerateFinished -= GetPCTurn;
//        //CellButton.OnPlayerClick -= GetPlayerTurn;
//        Game.finalUI.ActivateResults(result);
//        Service.BlockButtons();
//    }
//}
