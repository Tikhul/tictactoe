using UnityEngine;

public class TicTacToeModel : MonoBehaviour
{
    [SerializeField] private BoardModel _boardModel;
    [SerializeField] private PCModel _pc;
    [SerializeField] private HumanModel _human;
    [SerializeField] private GameModel _gameModel;

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
}
