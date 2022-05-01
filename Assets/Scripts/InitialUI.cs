using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InitialUI : TicTacToeElement
    // UI для начала игры
{
    private void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += ShowPlayersNames;
    }

    private void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= ShowPlayersNames;
    }

    private void ShowPlayersNames(string marker)
    {
        foreach (CreatePlayersButton i in FindObjectsOfType<CreatePlayersButton>())
        {
            string actualMarker = i.Marker;
            i.PlayerName.gameObject.SetActive(true);
            if (actualMarker.Equals(marker)) i.PlayerName.text = "Игрок";
            i.GetComponent<Button>().enabled = false;
        }
    }
}
