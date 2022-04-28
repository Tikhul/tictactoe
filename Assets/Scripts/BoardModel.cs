using System.Collections.Generic;
using UnityEngine;

public class BoardModel : TicTacToeElement
{
    public GameObject boardParent;
    public BoardScriptableObject boardSettings;
    public List<string> winCombinations;
    public List<CellButton> cellList;
    public string alphabet = "abcdefghijklmnopqrstuvwxyz";
}
