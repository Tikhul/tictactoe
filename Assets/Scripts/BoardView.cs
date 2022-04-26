using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : TicTacToeElement
{
    private void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += StartGame;
    }

    public delegate void StartGameAction();
    public static event StartGameAction OnGameStarted;
    void StartGame(string actualMarker)
    {
        OnGameStarted?.Invoke();
    }
}
