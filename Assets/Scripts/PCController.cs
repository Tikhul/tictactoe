using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : TicTacToeElement
{
    public delegate void GenerateAction(CellButton cell);
    public static event GenerateAction OnGenerateFinished;
    public void GeneratePCTurn()
    {
        IEnumerator coroutine = WaitPCTurn(2.0f);
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitPCTurn(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        System.Random rnd = new System.Random();
        if (game.boardModel.CellList.Count > 0)
        {
            int r = rnd.Next(game.boardModel.CellList.Count);
            CellButton chosenButton = game.boardModel.CellList[r];
            OnGenerateFinished(chosenButton);
        }
    }
}
