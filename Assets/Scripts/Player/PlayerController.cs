using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : TicTacToeElement
{

    void OnEnable()
    {
        BoardController.OnBoardCreated += CreatePlayers;
        CellButton.OnPlayerClick += UpdatePlayers;
        CellButton.OnPCTaken += UpdatePlayers;
    }

    void OnDisable()
    {
        CellButton.OnPlayerClick -= UpdatePlayers;
        CellButton.OnPCTaken -= UpdatePlayers;
    }

    public delegate void PlayersAction(string marker);
    public static event PlayersAction OnPlayersCreated;
    public void CreatePlayers(string actualMarker)
    {
        BoardController.OnBoardCreated -= CreatePlayers;
        game.human.isHuman = true;
        game.human.marker = actualMarker;

        game.pc.isHuman = false;
        game.pc.marker = PlayerModel.markerX;

        game.human.playerWins.AddRange(game.boardModel.winCombinations);
        game.pc.playerWins.AddRange(game.boardModel.winCombinations);


        if (actualMarker.Equals(PlayerModel.markerX)) game.pc.marker = PlayerModel.markerZero;

        OnPlayersCreated?.Invoke(actualMarker);
    }

    void UpdatePlayers(string actualMarker, int cellInt, char cellChar)
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

        if (game.human.playerTurns.Count >= game.boardModel.boardSettings.rowNumber / 2)
            DetectWinner(game.human.playerWins, game.human.playerTurns, game.human);
        else if (game.pc.playerTurns.Count >= game.boardModel.boardSettings.rowNumber / 2)
            DetectWinner(game.pc.playerWins, game.pc.playerTurns, game.pc);
    }

    void DetectWinner(List<string> wins, List<string> turns, PlayerModel player)
    {
        // При достаточном количестве ходов проверяю игру на выигрыш (человек)
            foreach (var win in wins)
            {
                int score = 0;
                foreach (var turn in turns)
                {
                    if (win.Contains(turn[0].ToString()) && win.Contains(turn[1].ToString())) score++;
                }

                if (score == game.boardModel.boardSettings.rowNumber)
                {
                    player.isWinner = true;
                    Debug.Log("winner detected");
                }
            }
    }

    void DestroyPlayers()
    {

    }
}
