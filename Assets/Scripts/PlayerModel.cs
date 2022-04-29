using System.Collections.Generic;

public class PlayerModel : TicTacToeElement
{
    public bool isHuman;
    public bool isWinner = false;
    public string marker;
    public static string markerX = "X";
    public static string markerZero = "0";
    public List<string> actualWinStrategy = new List<string>();
    public List<string> playerWins = new List<string>();
    public List<string> playerTurns = new List<string>();
}
