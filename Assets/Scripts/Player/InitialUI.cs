using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InitialUI : TicTacToeElement
    // UI для начала игры
{
    [SerializeField] private List<CreatePlayersButton> _buttons = new List<CreatePlayersButton>();
    public List<CreatePlayersButton> Buttons
    {
        get => _buttons;
        set => _buttons = value;
    }

    public void ShowPlayersNames(string marker)
    {
        foreach (CreatePlayersButton i in Buttons)
        {
            string actualMarker = i.Marker;
            i.PlayerName.gameObject.SetActive(true);
            if (actualMarker.Equals(marker)) i.PlayerName.text = "Игрок";
            i.GetComponent<Button>().enabled = false;
        }
    }
}
