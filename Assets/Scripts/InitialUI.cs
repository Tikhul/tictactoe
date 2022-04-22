using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialUI : MonoBehaviour
{
    public List<Button> initialButtons;
    public GameObject board;
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
        foreach (Button i in initialButtons)
        {
            string actualMarker = i.GetComponent<CreatePlayersButton>().marker;
            i.GetComponent<CreatePlayersButton>().playerName.gameObject.SetActive(true);
            if (actualMarker.Equals(marker)) i.GetComponent<CreatePlayersButton>().playerName.text = "Игрок";
            i.enabled = false;
            board.SetActive(true);
        }
    }
    
}
