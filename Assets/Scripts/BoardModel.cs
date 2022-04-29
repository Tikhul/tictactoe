using System.Collections.Generic;
using UnityEngine;

public class BoardModel : TicTacToeElement
{
    public GameObject boardParent;
    public BoardScriptableObject boardSettings;
    public List<List<CellButton>> winCombinations = new List<List<CellButton>>();
    public List<CellButton> cellList = new List<CellButton>();
    public string alphabet = "abcdefghijklmnopqrstuvwxyz";
}
