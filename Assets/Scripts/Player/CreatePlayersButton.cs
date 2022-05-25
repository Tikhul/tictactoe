using UnityEngine;
using TMPro;

public class CreatePlayersButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private string _marker;

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

    public delegate void ClickAction(string marker);
    public static event ClickAction OnPlayerChosen;

    public void PlayerChosen()
    {
        OnPlayerChosen?.Invoke(Marker);
    }
}
