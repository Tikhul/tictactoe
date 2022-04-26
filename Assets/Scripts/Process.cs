using UnityEngine;

public class Process : Player
{
    public static Player human;
    public static Player pc;

    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += StartGame;
        CellButton.OnPlayerClick += GeneratePCTurn;
        CellButton.OnPlayerClick += AfterClick;
        CellButton.OnPCTaken += AfterClick;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= StartGame;
        CellButton.OnPlayerClick -= GeneratePCTurn;
        CellButton.OnPlayerClick -= AfterClick;
        CellButton.OnPCTaken -= AfterClick;
    }

    void StartGame(string actualMarker)
    {
        human = new Player(true, actualMarker);
        pc = new Player(false, markerX);

        if (actualMarker.Equals(markerX)) pc.marker = markerZero;

        CreateBoard();

        human.playerWins.AddRange(winCombinations);
        pc.playerWins.AddRange(winCombinations);

        PlayGame(actualMarker);
    }

    void PlayGame(string actualMarker)
    {
        if (actualMarker.Equals(markerZero)) GeneratePCTurn(actualMarker, 1, 'A');
    }

    void AfterClick(string actualMarker, int cellInt, char cellChar)
    {
        CellsAfterTurn(cellInt, cellChar);

        if (actualMarker.Equals(human.marker))
        {
            Debug.Log(actualMarker + human.marker);
            pc.playerWins = CheckWinCombinations(pc.playerWins, cellInt, cellChar);
            human.playerTurns.Add(cellInt.ToString()+ cellChar.ToString());
        }
        else if (actualMarker.Equals(pc.marker))
        {
            Debug.Log(actualMarker + pc.marker);
            human.playerWins = CheckWinCombinations(human.playerWins, cellInt, cellChar);
            pc.playerTurns.Add(cellInt.ToString() + cellChar.ToString());
        }
    } 

    void FinishGame()
    {

    }
}
