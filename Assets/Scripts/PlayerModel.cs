using System.Collections.Generic;

public class PlayerModel : TicTacToeElement
{
    public bool isHuman;
    public bool isWinner = false;
    public string marker;
    public static string markerX = "X";
    public static string markerZero = "0";
    public List<List<CellButton>> actualWinStrategy = new List<List<CellButton>>();
    public List<List<CellButton>> playerWins = new List<List<CellButton>>();
    public List<CellButton> playerTurns = new List<CellButton>();
}
