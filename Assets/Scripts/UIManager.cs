using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Game;
    public GameObject Shop;
    public InputField inputName, roomCode;
    private FireBaseManager fbManager;
    public Button playBtn, joinBtn;
    public TMP_Text score1, score2, playerName1, playerName2, winner;
    public Text timer;

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

    public void Purchase()
    {
        Shop.SetActive(true);
    }
}
