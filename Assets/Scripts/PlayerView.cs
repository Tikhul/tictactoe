using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
        foreach (CreatePlayersButton i in GetComponents<CreatePlayersButton>())
        {
            string actualMarker = i.marker;
            i.playerName.gameObject.SetActive(true);
            if (actualMarker.Equals(marker)) i.playerName.text = "Игрок";
            gameObject.GetComponent<Button>().enabled = false;
        }
    }

    void BlockButtons(string marker, int cellInt, char cellChar)
    {
        foreach (var cell in GetComponents<CellButton>())
        {
            gameObject.GetComponent<Button>().enabled = false;
        }
    }

    void ActivateButtons(string marker, int cellInt, char cellChar)
    {
        foreach (var cell in GetComponents<CellButton>())
        {
            if (!cell.taken) gameObject.GetComponent<Button>().enabled = true;
        }
    }
}
