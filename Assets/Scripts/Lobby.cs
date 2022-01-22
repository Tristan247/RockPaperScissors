using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Lobby
{
    public User player1, player2;
    public bool lobbyFull;
    public bool startGame;
    public string winner;
    public int timer;

}
