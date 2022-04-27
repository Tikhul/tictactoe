using System.Collections.Generic;

public class PlayerModel : TicTacToeElement
{
    public bool isHuman;
    public bool isWinner = false;
    public string marker;
    public static string markerX = "X";
    public static string markerZero = "0";
    public List<string> playerWins = new List<string>();
    public List<string> playerTurns = new List<string>();
    public bool IsHuman { get; set; }
    public string Marker
    {
        get { return marker; }
        set
        {
            if (value.Equals(markerZero) || value.Equals(markerX)) marker = value;
        }
    }

    public PlayerModel()
    {

    }
    public PlayerModel(bool isHuman, string marker)
    {
        IsHuman = isHuman;
        Marker = marker;
    }
}
