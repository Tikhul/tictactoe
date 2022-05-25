using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : TicTacToeElement
{
    public delegate void HumanTurnAction();
    public static event HumanTurnAction OnHumanTurn;
    private void GetHumanTurn(CellButton cell)
    {
     //   if (!game.gameStateController.finishedGame) OnHumanTurn?.Invoke();
    }
}
