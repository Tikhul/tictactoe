using UnityEngine;
using TMPro;

public class CreatePlayersButton : MonoBehaviour
{
    public TMP_Text playerName;
    public string marker;

    public delegate void ClickAction(string marker);
    public static event ClickAction OnPlayerChosen;

    public void PlayerChosen()
    {
        OnPlayerChosen(marker);
    }
}
