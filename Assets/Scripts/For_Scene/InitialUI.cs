using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialUI : MonoBehaviour
{
    public List<Button> initialButtons;
    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += ShowPlayersNames;
        CellButton.OnCellHumanClicked += DeactivateButtons;
        CellButton.OnCellPCTaken += ActivateButtons;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= ShowPlayersNames;
        CellButton.OnCellHumanClicked += DeactivateButtons;
        CellButton.OnCellPCTaken += ActivateButtons;
    }

    void ShowPlayersNames(string marker)
    {
        foreach (Button i in initialButtons)
        {
            string actualMarker = i.GetComponent<CreatePlayersButton>().marker;
            i.GetComponent<CreatePlayersButton>().playerName.gameObject.SetActive(true);
            if (actualMarker.Equals(marker)) i.GetComponent<CreatePlayersButton>().playerName.text = "Игрок";
            i.enabled = false;
        }
    }

    void DeactivateButtons(string marker, int cellInt, char cellChar)
    {
        CellButton[] buttons = FindObjectsOfType<CellButton>();
        foreach (var button in buttons)
            if (button.GetComponent<Button>())
                button.GetComponent<Button>().enabled = false;
    }

    void ActivateButtons(string marker, int cellInt, char cellChar)
    {
        CellButton[] buttons = FindObjectsOfType<CellButton>();
        foreach (var button in buttons)
            if (button.GetComponent<Button>() && !button.taken)
                button.GetComponent<Button>().enabled = true;
    }
}
