using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField _roomNameipField;
    public GameObject _roomPanel, _lobbyPanel;
    public TMP_Text _roomName;

    public Room _roomItem;
    List<Room> _roomsAvailable = new List<Room>();
    public Transform _scrollContent;

    [SerializeField]
    List<PlayerCardManager> playersList = new List<PlayerCardManager>();
    public  PlayerCardManager playerCardPrefab;
    public GameObject _playButton;
    public Transform playerCardsHolder;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoomOnClick()
    {
        if(_roomNameipField.text.Length > 0)
        {
            PhotonNetwork.CreateRoom(_roomNameipField.text, new RoomOptions { MaxPlayers = 6});
        }
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            _playButton.SetActive(true);
        }
        else
        {
            _playButton.SetActive(false);
        }
    }

    public void OnPlayButtonClick()
    {
        PhotonNetwork.LoadLevel("Menu");
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnJoinedRoom()
    {
        _lobbyPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public void LeaveRoomOnClick()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnLeftRoom()
    {
        _lobbyPanel.SetActive(true);
        _roomPanel.SetActive(false);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    public void UpdateRoomList(List<RoomInfo> list)
    {
        foreach(Room room in _roomsAvailable)
        {
            Destroy(room.gameObject);
        }
        _roomsAvailable.Clear();

        foreach(RoomInfo room in list)
        {
            Room newRoom = Instantiate(_roomItem, _scrollContent);
            newRoom.SetRoomName(room.Name);
            _roomsAvailable.Add(newRoom);
        }
    }

    public void UpdatePlayerList()
    {
        foreach(PlayerCardManager playercard in playersList)
        {
            Debug.Log("player card name : " + playercard.name);
            Destroy(playercard.gameObject);
        }
        playersList.Clear();
        if (PhotonNetwork.CurrentRoom == null)
            return;
        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerCardManager newPlayerCard = Instantiate(playerCardPrefab, playerCardsHolder);
            newPlayerCard.SetPlayerInfo(player.Value);
            playersList.Add(newPlayerCard);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
