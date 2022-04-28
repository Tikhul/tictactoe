using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : TicTacToeElement
{

    void OnEnable()
    {
        BoardController.OnBoardCreated += CreatePlayers;
        CellButton.OnPlayerClick += UpdatePlayers;
        CellButton.OnPlayerClick += GeneratePCTurn;
        CellButton.OnPCTaken += UpdatePlayers;
    }

    void OnDisable()
    {
        CellButton.OnPlayerClick -= UpdatePlayers;
        CellButton.OnPlayerClick -= GeneratePCTurn;
        CellButton.OnPCTaken -= UpdatePlayers;
    }

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

        LaunchFirstTurn(actualMarker);
    }

    void UpdatePlayers(string actualMarker, int cellInt, char cellChar)
    {
        if (actualMarker.Equals(game.human.marker))
        {
            CheckWinCombinations(game.pc.playerWins, cellInt, cellChar);
            game.human.playerTurns.Add(cellChar.ToString() + cellInt.ToString());
        }
        else if (actualMarker.Equals(game.pc.marker))
        {
            CheckWinCombinations(game.human.playerWins, cellInt, cellChar);
            game.pc.playerTurns.Add(cellChar.ToString() + cellInt.ToString());
        }

      ///  Debug.Log("Human win combinations");
      //  foreach (var win in game.human.playerWins) Debug.Log(win);
      //  Debug.Log("PC win combinations");
      //  foreach (var win in game.pc.playerWins) Debug.Log(win);
    }

    void CheckWinCombinations(List<string> wins, int cellInt, char cellChar)
    {
        List<string> tempList = new List<string>();

        foreach (var win in wins)
        {
            Debug.Log(win);
            for (int i = 0; i < win.Length - 1; i+=2)
            {
                Debug.Log(i);
                if (win[i].ToString().Contains(cellChar.ToString()) && win[i + 1].ToString().Contains(cellInt.ToString()))
                {
                    tempList.Add(win);
                    Debug.Log(win[i].ToString() + win[i + 1].ToString());
                }     
            }
        }
        wins.RemoveAll(item => tempList.Contains(item));
        CheckTurn();
    }

    void CheckTurn()
    {
        if (game.human.playerTurns.Count >= game.boardModel.boardSettings.rowNumber / 2)
            DetectWinner(game.human.playerWins, game.human.playerTurns, game.human);

        else if (game.pc.playerTurns.Count >= game.boardModel.boardSettings.rowNumber / 2)
            DetectWinner(game.pc.playerWins, game.pc.playerTurns, game.pc);
    }

    void LaunchFirstTurn(string actualMarker)
    {
        if (actualMarker.Equals(PlayerModel.markerZero)) GeneratePCTurn(actualMarker, 1, 'A');
        //     PlayerController.OnPlayersCreated -= LaunchFirstTurn;
    }

    public delegate void GenerateAction(CellButton button);
    public static event GenerateAction OnTurnGenerated;
    public void GeneratePCTurn(string actualMarker, int cellInt, char cellChar)
    {
        Service.BlockButtons(actualMarker, cellInt, cellChar);
        IEnumerator coroutine = WaitPCTurn(2.0f);
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
