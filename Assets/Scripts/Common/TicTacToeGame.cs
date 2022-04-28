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
    public PlayerModel pc;
    public PlayerModel human;

    public BoardController boardController;
    public PlayerController playerController;
    public PCController pcController;
    public GameStateController gameStateController;
    public StepExecutionController stepExecutionController;

    public InitialUI initialUI;
    public FinalUI finalUI;
}
