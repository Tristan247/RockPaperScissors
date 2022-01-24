using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Sprite Rock, Paper, Scissors;
    public int player1Score, player2Score;
    public string player1Choice, player2Choice;

    public void Play()
    {
        switch (player1Choice)
        {
            case "Rock":
                switch (player2Choice)
                {
                    case "Rock":
                        player1Score = player1Score + 1;
                        player2Score = player2Score + 1;
                        break;

                    case "Paper":
                        player2Score = player2Score + 1;
                        break;

                    case "Scissors":
                        player1Score = player1Score + 1;
                        break;
                }
                break;

            case "Paper":
                switch (player2Choice)
                {
                    case "Rock":
                        player1Score = player1Score + 1;
                        break;

                    case "Paper":
                        player1Score = player1Score + 1;
                        player2Score = player2Score + 1;
                        break;

                    case "Scissors":
                        player2Score = player2Score + 1;
                        break;
                }
                break;

            case "Scissors":
                switch (player2Choice)
                {
                    case "Rock":
                        player2Score = player2Score + 1;
                        break;

                    case "Paper":
                        player1Score = player1Score + 1;
                        break;

                    case "Scissors":
                        player1Score = player1Score + 1;
                        player2Score = player2Score + 1;
                        break;
                }
                break;
        }
    }
}
