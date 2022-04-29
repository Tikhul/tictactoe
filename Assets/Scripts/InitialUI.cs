using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InitialUI : TicTacToeElement

    // UI для начала игры
{
    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += ShowPlayersNames;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= ShowPlayersNames;
    }

    void ShowPlayersNames(string marker)
    {
        foreach (CreatePlayersButton i in FindObjectsOfType<CreatePlayersButton>())
        {
            string actualMarker = i.marker;
            i.playerName.gameObject.SetActive(true);
            if (actualMarker.Equals(marker)) i.playerName.text = "Игрок";
            i.GetComponent<Button>().enabled = false;
        }
    }
}
