using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : TicTacToeElement
{

    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += CreatePlayers;
        CellButton.OnPlayerClick += GeneratePCTurn;
        CellButton.OnPlayerClick += AfterTurn;
        CellButton.OnPCTaken += AfterTurn;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= CreatePlayers;
        CellButton.OnPlayerClick -= GeneratePCTurn;
        CellButton.OnPlayerClick -= AfterTurn;
        CellButton.OnPCTaken -= AfterTurn;
    }

    public void CreatePlayers(string actualMarker)
    {
        game.human.isHuman = true;
        game.human.marker = actualMarker;

        game.pc.isHuman = false;
        game.pc.marker = PlayerModel.markerX;

        if (actualMarker.Equals(PlayerModel.markerX)) game.pc.marker = PlayerModel.markerZero;

        game.boardController.CreateBoard();

        game.human.playerWins.AddRange(game.boardModel.winCombinations);
        game.pc.playerWins.AddRange(game.boardModel.winCombinations);

        LaunchFirstTurn(actualMarker);
    }

    void LaunchFirstTurn(string actualMarker)
    {
        if (actualMarker.Equals(PlayerModel.markerZero)) GeneratePCTurn(actualMarker, 1, 'A');
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

    // После хода игрока
    public void AfterTurn(string actualMarker, int cellInt, char cellChar)
    {
        if (actualMarker.Equals(game.human.marker))
        {
            game.pc.playerWins = game.boardController.CheckWinCombinations(game.pc.playerWins, cellInt, cellChar);
            game.human.playerTurns.Add(cellInt.ToString() + cellChar.ToString());
        }
        else if (actualMarker.Equals(game.pc.marker))
        {
            game.human.playerWins = game.boardController.CheckWinCombinations(game.human.playerWins, cellInt, cellChar);
            game.pc.playerTurns.Add(cellInt.ToString() + cellChar.ToString());
        }

        DetectWinner();
    }
        
    void DetectWinner()
    {
        // При достаточном количестве ходов проверяю игру на выигрыш (человек)
        if (game.human.playerTurns.Count >= game.boardModel.boardSettings.rowNumber / 2)
        {
            foreach (var win in game.human.playerWins)
            {
                int score = 0;
                foreach (var turn in game.human.playerTurns)
                {
                    if (win.Contains(turn[0].ToString()) && win.Contains(turn[1].ToString())) score++;

                }
                
                if (score == game.boardModel.boardSettings.rowNumber)
                {
                    game.human.isWinner = true;
                    Debug.Log("Human wins");
                }
            }
        }

        // При достаточном количестве ходов проверяю игру на выигрыш (пк)
        if (game.pc.playerTurns.Count >= game.boardModel.boardSettings.rowNumber / 2)
        {
            foreach (var win in game.pc.playerWins)
            {
                int score = 0;

                foreach (var turn in game.pc.playerTurns)
                {
                    if (win.Contains(turn[0].ToString()) && win.Contains(turn[1].ToString())) score++;

                }
                if (score == game.boardModel.boardSettings.rowNumber)
                {
                    game.human.isWinner = true;
                    Debug.Log("PC wins");
                }
            }
        }
    }
}
