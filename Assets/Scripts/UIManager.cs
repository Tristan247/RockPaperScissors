using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Game;
    public InputField inputName, roomCode;
    private FireBaseManager fbManager;
    public Button playBtn, joinBtn;

    void Start()
    {
        fbManager = FindObjectOfType<FireBaseManager>();
    }


    void Update()
    {
        
    }

    public void Play()
    {
        fbManager.CreateGame();
        playBtn.interactable = false;
        joinBtn.interactable = false;
    }

    public void Join()
    {
        fbManager.JoinGame();
        playBtn.interactable = false;
        joinBtn.interactable = false;
    }
}
