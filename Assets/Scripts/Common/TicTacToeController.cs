using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeController : MonoBehaviour
{
    [SerializeField] private BoardController _boardController;
    [SerializeField] private PCController _pcController;
    [SerializeField] private HumanController _humanController;
    [SerializeField] private GameController _gameController;

    public BoardController BoardController
    {
        get => _boardController;
        set => _boardController = value;
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
}
