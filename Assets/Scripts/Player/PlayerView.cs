using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerView : TicTacToeElement
{
    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += ShowPlayersNames;
        CellButton.OnPlayerClick += BlockButtons;
        CellButton.OnPCTaken += ActivateButtons;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= ShowPlayersNames;
        CellButton.OnPlayerClick -= BlockButtons;
        CellButton.OnPCTaken += ActivateButtons;
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

    void BlockButtons(string marker, int cellInt, char cellChar)
    {
        foreach (var cell in FindObjectsOfType<CellButton>())
        {
            if(cell.GetComponent<Button>()) cell.GetComponent<Button>().enabled = false;
        }
    }

    void ActivateButtons(string marker, int cellInt, char cellChar)
    {
        foreach (var cell in FindObjectsOfType<CellButton>())
        {
            if (cell.GetComponent<Button>() && !cell.taken) cell.GetComponent<Button>().enabled = true;
        }
    }
}
