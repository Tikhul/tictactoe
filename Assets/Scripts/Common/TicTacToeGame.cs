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

    public BoardController boardController;
    public PlayerController playerController;
    public PCController pcController;
    public HumanController humanController;
    public GameStateController gameStateController;
    public CellButtonController cellButtonController;
    public StepExecutionController stepExecutionController;

    public InitialUI initialUI;
    public FinalUI finalUI;
}
