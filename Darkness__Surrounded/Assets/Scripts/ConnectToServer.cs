using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField _playerNameipField;
    public Button _connectBtn;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ConnectToMasterOnClick()
    {
        if(_playerNameipField.text.Length > 0)
        {
            PhotonNetwork.NickName = _playerNameipField.text;
            _connectBtn.GetComponentInChildren<Text>().text = "Connecting....";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    // Update is called once per frame
    void Update()
    {
       // SceneManager.LoadScene("Lobby");
    }
}
