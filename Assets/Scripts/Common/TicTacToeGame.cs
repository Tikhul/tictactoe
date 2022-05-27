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
    [SerializeField] private TicTacToeModel _model;
    [SerializeField] private TicTacToeController _controller;
    [SerializeField] private TicTacToeView _view;
    public TicTacToeModel TicTacToeModel
    {
        get => _model;
        set => _model = value;
    }

    public TicTacToeController TicTacToeController
    {
        get => _controller;
        set => _controller = value;
    }

    public TicTacToeView TicTacToeView
    {
        get => _view;
        set => _view = value;
    }
}
