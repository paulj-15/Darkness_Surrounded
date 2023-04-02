using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room : MonoBehaviour
{
    public TMP_Text _roomName;

    [SerializeField]
    LobbyManager _lobbyManager;

    public void SetRoomName(string _roomNameParam)
    {
        _roomName.text = _roomNameParam;
    }
    // Start is called before the first frame update
    void Start()
    {
        _lobbyManager = FindObjectOfType<LobbyManager>();
    }

    public void JoinRoomOnClick()
    {
        _lobbyManager.JoinRoom(_roomName.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
