using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeElement : MonoBehaviour
{
    private TicTacToeGame _game;
    public TicTacToeGame game
    {
        get {
            if (_game == null) _game = FindObjectOfType<TicTacToeGame>();
            return _game; 
        }
    }
}

public class TicTacToeGame : MonoBehaviour
{
    public TicTacToeModel model;
    public TicTacToeController controller;
    public TicTacToeView view;
}

public class TicTacToeModel : MonoBehaviour
{
    public BoardModel boardModel;
    public PlayerModel pc;
    public PlayerModel human;
}

public class TicTacToeController : MonoBehaviour
{
    public BoardController boardController;
    public PlayerController playerController;
    public PCController pcController;
    public GameStateController gameStateController;
    public StepExecutionController stepExecutionController;
}

public class TicTacToeView : MonoBehaviour
{
    public InitialUI initialUI;
    public FinalUI finalUI;
}