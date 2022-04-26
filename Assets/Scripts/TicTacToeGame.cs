using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeElement : MonoBehaviour
{
    public TicTacToeGame game { get { return FindObjectOfType<TicTacToeGame>(); } }
}
public class TicTacToeGame : MonoBehaviour
{
    public BoardModel boardModel;
    public BoardView boardView;
    public BoardController boardController;

    public PlayerModel human;
    public PlayerModel pc;
    public PlayerView playerView;
    public PlayerController playerController;
}
