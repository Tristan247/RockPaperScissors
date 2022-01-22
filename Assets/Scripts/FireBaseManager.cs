using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class FireBaseManager : MonoBehaviour
{
    Lobby myLobby;
    private string key;
    DatabaseReference reference;
    private UIManager uiManager;


    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Update()
    {
        
    }

    public void CreateGame()
    {
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
              }
          });
        uiManager.MainMenu.SetActive(false);
    }

    void LobbyListener(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        myLobby = JsonUtility.FromJson<Lobby>(args.Snapshot.GetRawJsonValue());
    }
}
