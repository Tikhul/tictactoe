using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeElement : MonoBehaviour
{
    private TicTacToeGame _game;
    public TicTacToeGame Game 
    { get 
        {
            if (_game == null) _game = FindObjectOfType<TicTacToeGame>();
            return _game;
        } 
    }
}
public class TicTacToeGame : MonoBehaviour
{
    [SerializeField] private BoardModel _boardModel;
    [SerializeField] private PCModel _pc;
    [SerializeField] private HumanModel _human;
    [SerializeField] private GameModel _gameModel;

    [SerializeField] private BoardController _boardController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PCController _pcController;
    [SerializeField] private HumanController _humanController;
    [SerializeField] private GameController _gameController;

    [SerializeField] private InitialUI _initialUI;
    [SerializeField] private FinalUI _finalUI;

    public BoardModel BoardModel
    {
        get => _boardModel;
        set => _boardModel = value;
    }
    public PCModel PCModel
    {
        get => _pc;
        set => _pc = value;
    }
    public HumanModel HumanModel
    {
        get => _human;
        set => _human = value;
    }
    public GameModel GameModel
    {
        get => _gameModel;
        set => _gameModel = value;
    }
    public BoardController BoardController
    {
        get => _boardController;
        set => _boardController = value;
    }
    public PlayerController PlayerController
    {
        get => _playerController;
        set => _playerController = value;
    }
    public PCController PCController
    {
        get => _pcController;
        set => _pcController = value;
    }
    public HumanController HumanController
    {
        get => _humanController;
        set => _humanController = value;
    }
    public GameController GameController
    {
        get => _gameController;
        set => _gameController = value;
    }
    public InitialUI InitialUI
    {
        get => _initialUI;
        set => _initialUI = value;
    }
    public FinalUI FinalUI
    {
        get => _finalUI;
        set => _finalUI = value;
    }
}
