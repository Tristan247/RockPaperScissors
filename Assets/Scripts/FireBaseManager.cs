using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class FireBaseManager : MonoBehaviour
{
    Lobby myLobby;
    public string key;
    DatabaseReference reference;
    private UIManager uiManager;
    Game game;


    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        game = FindObjectOfType<Game>();
    }

    void Update()
    {
        
    }

    public void CreateGame()
    {
        game.player = false;
        myLobby = new Lobby();
        User user = new User(uiManager.inputName.text);
        myLobby.player1 = user;

        key = reference.Child("lobbies").Push().Key;

        reference.Child("lobbies").Child(key).SetRawJsonValueAsync(JsonUtility.ToJson(myLobby));

        FirebaseDatabase.DefaultInstance
        .GetReference("lobbies/" + key)
        .ValueChanged += LobbyListener;

        uiManager.roomCode.text = key;
        uiManager.roomCode.readOnly = true;
        uiManager.MainMenu.SetActive(true);
    }

    public void JoinGame()
    {
        game.player = true;
        key = uiManager.roomCode.text;
        FirebaseDatabase.DefaultInstance
          .GetReference("lobbies/" + key)
          .GetValueAsync().ContinueWith(task => {
              if (task.IsFaulted)
              {
                  Debug.Log(task.IsFaulted);
              }
              else if (task.IsCompleted)
              {
                  Debug.Log("Ok");
                  User user = new User(uiManager.inputName.text);
                  FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + key + "/player2").SetRawJsonValueAsync(JsonUtility.ToJson(user));
                  FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + key + "/startGame").SetValueAsync(true);
              }
          });
        uiManager.MainMenu.SetActive(false);
        uiManager.Game.SetActive(true);

        FirebaseDatabase.DefaultInstance
        .GetReference("lobbies/" + key)
        .ValueChanged += LobbyListener;
    }

    void LobbyListener(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        myLobby = JsonUtility.FromJson<Lobby>(args.Snapshot.GetRawJsonValue());

        if(!uiManager.Game.activeSelf && myLobby.startGame)
        {
            uiManager.MainMenu.SetActive(false);
            uiManager.Game.SetActive(true);
        }

        uiManager.score1.text = myLobby.player1.score.ToString();
        uiManager.score2.text = myLobby.player2.score.ToString();
        uiManager.playerName1.text = myLobby.player1.name;
        uiManager.playerName2.text = myLobby.player2.name;
        uiManager.winner.text = myLobby.winner;
        uiManager.timer.text = myLobby.timer.ToString();
    }

    public void Buttons(string choice)
    {
        if(game.player == false)
        {
            myLobby.player1.choice = choice;
            FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + key + "/player1/choice").SetValueAsync(myLobby.player1.choice);
        }
        else
        {
            myLobby.player2.choice = choice;
            FirebaseDatabase.DefaultInstance.GetReference("lobbies/" + key + "/player2/choice").SetValueAsync(myLobby.player2.choice);
        }
    }
}
