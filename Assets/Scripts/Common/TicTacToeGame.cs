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
    public PCModel pc;
    public HumanModel human;
    public GameModel gameModel;

    public BoardController boardController;
    public PlayerController playerController;
    public PCController pcController;
    public HumanController humanController;
    public GameController gameController;

    public InitialUI initialUI;
    public FinalUI finalUI;
}
