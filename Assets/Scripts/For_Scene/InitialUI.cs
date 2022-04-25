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
    }

    void OnDisable()
    {
        CreatePlayersButton.OnPlayerChosen -= ShowPlayersNames;
        CellButton.OnPlayerClick -= BlockAllButtons;
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
        CellButton[] cellButtons = FindObjectsOfType<CellButton>();
        foreach(var cell in cellButtons)
        {
            if(cell.GetComponent<Button>()) cell.GetComponent<Button>().enabled = false;
        }
    }
}
