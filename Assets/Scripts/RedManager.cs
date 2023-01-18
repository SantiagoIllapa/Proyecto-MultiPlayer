using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

namespace Com.Saint.MyGame
{
    public class RedManager : MonoBehaviourPunCallbacks
    {
        public TMP_InputField playerNameMP;
        public TMP_InputField NombreSalaMP;
        public GameObject panel;
        public string gameVersion;
        private string playerName;


        void Start()
        {
            ActivatePanel("Panel Ingreso");
        }

        void Update()
        {

        }
        public void FixedUpdate()
        {
            
        }
        public void UserLogin()
        {
            playerName = playerNameMP.text;
            if (!string.IsNullOrEmpty(playerName))
            {
                ActivatePanel("Panel Conectando");
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.LocalPlayer.NickName=playerName;
            }
            else
            {
                Debug.LogError("El nombre del jugador no es válido");
            }
        }
        public override void OnConnected()
        {
            Debug.Log("Conectado a la red");
        }
        public override void OnConnectedToMaster()
        {
            ActivatePanel("Panel Opciones");

            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " se encuentra conectado al photon");
        }
        public void ActivatePanel(string panel_name)
        {
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                panel.transform.GetChild(i).gameObject.SetActive(panel.transform.GetChild(i).name.Equals(panel_name));

            }
        }
        public void CancelePanelSalaCreation()
        {
            ActivatePanel("Panel Opciones");
        }
        public void OnClickBtnCrearSala()
        {
            PhotonNetwork.JoinRandomRoom();
           
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            int radID = Random.Range(0, 3000);
            PhotonNetwork.CreateRoom("Sala "+ radID, new RoomOptions { MaxPlayers = 8 },TypedLobby.Default);
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
       
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }
        public override void OnCreatedRoom()
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name + " creada");
        }
        public override void OnJoinedRoom()
        {
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " se ha unido a la sala " + PhotonNetwork.CurrentRoom.Name);
            //panel.SetActive(false);q11111111111111111111111111111111111111111111111111111111111111'0
            PhotonNetwork.LoadLevel("Lobby");
            //GameObject player = PhotonNetwork.Instantiate("Player",Vector3.zero,Quaternion.identity,0);
            //player.GetComponent<PlayerManager>().username = PhotonNetwork.LocalPlayer.NickName;
            

        }
      
    }

}