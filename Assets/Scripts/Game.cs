using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class Game : MonoBehaviour
{
    public Sprite Rock, Paper, Scissors;
    public int player1Score, player2Score;
    public string player1Choice, player2Choice;
    public bool player;
    DatabaseReference reference;
    Lobby tempLobby;
    public FireBaseManager fbManager;

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void Play()
    {
        Debug.Log("Hello1");
        reference.GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                tempLobby = JsonUtility.FromJson<Lobby>(snapshot.GetRawJsonValue());
                player1Choice = tempLobby.player1.choice;
                player2Choice = tempLobby.player2.choice;
            }
        });

        switch (player1Choice)
        {
            case "Rock":
                switch (player2Choice)
                {
                    case "Rock":
                        player1Score = player1Score + 1;
                        player2Score = player2Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player1/score").SetValueAsync(player1Score);
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player2/score").SetValueAsync(player2Score);
                        break;

                    case "Paper":
                        player2Score = player2Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player2/score").SetValueAsync(player2Score);
                        break;

                    case "Scissors":
                        player1Score = player1Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player1/score").SetValueAsync(player1Score);
                        break;
                }
                break;

            case "Paper":
                switch (player2Choice)
                {
                    case "Rock":
                        player1Score = player1Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player1/score").SetValueAsync(player1Score);
                        break;

                    case "Paper":
                        player1Score = player1Score + 1;
                        player2Score = player2Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player1/score").SetValueAsync(player1Score);
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player2/score").SetValueAsync(player2Score);
                        break;

                    case "Scissors":
                        player2Score = player2Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player2/score").SetValueAsync(player2Score);
                        break;
                }
                break;

            case "Scissors":
                switch (player2Choice)
                {
                    case "Rock":
                        player2Score = player2Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player2/score").SetValueAsync(player2Score);
                        break;

                    case "Paper":
                        player1Score = player1Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player1/score").SetValueAsync(player1Score);
                        break;

                    case "Scissors":
                        player1Score = player1Score + 1;
                        player2Score = player2Score + 1;
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player2/score").SetValueAsync(player2Score);
                        FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + fbManager.key + "/player1/score").SetValueAsync(player1Score);
                        break;
                }
                break;
        }
    }
}
