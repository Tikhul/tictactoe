using UnityEngine;
using TMPro;

public class CreatePlayersButton : TicTacToeElement
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private string _marker;
    public event System.Action<string> OnPlayerChosen = delegate { };

    public TMP_Text PlayerName
    {
        get => _playerName;
        set => _playerName = value;
    }

    public string Marker
    {
        get => _marker;
        set => _marker = value;
    }
    
    public void PlayerChosen()
    {
        OnPlayerChosen(Marker);
        Game.TicTacToeView.InitialUI.ShowPlayersNames(Marker);
        Game.TicTacToeController.BoardController.CreateBoard(Marker);
    }
}
