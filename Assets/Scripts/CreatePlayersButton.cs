using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayersButton : MonoBehaviour
{
    public void CreatePlayers(string text)
    {
        Player player = new Player();
        player.CreateTwoPlayers(text);
    }
}
