using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepExecutionController : TicTacToeElement
{
    private void OnEnable()
    {
        PlayerController.OnPlayersCreated += LaunchFirstTurn;
        CellButton.OnPlayerClick += GeneratePCTurn;
    }

    private void OnDisable()
    {
        CellButton.OnPlayerClick -= GeneratePCTurn;
    }

    void LaunchFirstTurn(string actualMarker)
    {
        if (actualMarker.Equals(PlayerModel.markerZero)) GeneratePCTurn(actualMarker, 1, 'A');
        PlayerController.OnPlayersCreated -= LaunchFirstTurn;
    }

    public delegate void GenerateAction(CellButton button);
    public static event GenerateAction OnTurnGenerated;
    public void GeneratePCTurn(string actualMarker, int cellInt, char cellChar)
    {
        IEnumerator coroutine = WaitPCTurn(1.0f);
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitPCTurn(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        System.Random rnd = new System.Random();
        if (game.boardModel.cellList.Count > 0)
        {
            int r = rnd.Next(game.boardModel.cellList.Count);
            CellButton chosenButton = game.boardModel.cellList[r];
            OnTurnGenerated(chosenButton);
        }
    }
}
