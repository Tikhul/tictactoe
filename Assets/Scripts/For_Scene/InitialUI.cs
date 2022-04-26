using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialUI : MonoBehaviour
{
    public List<Button> initialButtons;
    
    void OnEnable()
    {
        CreatePlayersButton.OnPlayerChosen += ShowPlayersNames;
        CellButton.OnPlayerClick += BlockAllButtons;
        CellButton.OnPCTaken += ActivateButtons;
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= ShowPlayersNames;
        CellButton.OnPlayerClick -= BlockAllButtons;
        CellButton.OnPCTaken += ActivateButtons;
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

    void BlockAllButtons(string marker, int cellInt, char cellChar)
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
