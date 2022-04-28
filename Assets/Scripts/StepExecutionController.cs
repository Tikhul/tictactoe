using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepExecutionController : TicTacToeElement
{
    // Выполнение шагов человека и ПК, запуск проверок после шага
    private void OnEnable()
    {
        CellButton.OnPlayerClick += GetPlayerTurn;
        PCController.OnGenerateFinished += GetPCTurn;
    }

    public void LaunchFirstTurn(string actualMarker)
    {
        if (actualMarker.Equals(PlayerModel.markerZero)) LaunchPCTurn();
    }

    void LaunchPCTurn() 
    {
        Service.BlockButtons();
        game.pcController.GeneratePCTurn();
    } 

    void GetPCTurn(CellButton cell)
    {
        TurnExecuted(game.pc.marker, cell);
        Service.ActivateButtons();
    }

    void GetPlayerTurn(CellButton cell)
    {
        TurnExecuted(game.human.marker, cell);
        LaunchPCTurn();
    }

    void TurnExecuted(string actualMarker, CellButton cell)
    {
        game.gameStateController.CheckGameState(actualMarker, cell);
    }

    public void OutOfTurns()
    {
        Service.BlockButtons();
        CellButton.OnPlayerClick -= GetPlayerTurn;
        PCController.OnGenerateFinished -= GetPCTurn;
    }
}
