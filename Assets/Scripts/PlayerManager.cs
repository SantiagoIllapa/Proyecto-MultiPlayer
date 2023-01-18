using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon;


namespace Com.Saint.MyGame
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI userText;
        public Rigidbody rg;
        public float speed = 1;
        public float turnspeed = 1; 
        public float jumpForce = 1;
        private float x, y;
        public string username;
        public GameObject panelVictory;
        public static GameObject LocalPlayerInstance;
        public TextMeshProUGUI upointsText;
        private int points=0;


        void Start()
        {
            userText.text = username;
            rg = GetComponent<Rigidbody>();
            if (photonView.IsMine)
            {
               
            }
            else
            {

            }
        }
        void Awake()
        {
            
             if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            DontDestroyOnLoad(this.gameObject);
        }

        void Update()
        {
            
            if (photonView.IsMine)
            {
                
                
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
                //x = Input.GetAxis("Horizontal");
                //y = Input.GetAxis("Vertical");

                //Vector3 mov = new(0, 0, movimientoVerti);
                ////transform.Rotate(Vector3.up, movimientoHori* turnspeed*Time.deltaTime);
                //transform.Translate(Vector3.forward * Time.deltaTime * speed * movimientoVerti);
                //transform.Translate(Vector3.right * Time.deltaTime * speed * movimientoHori);
                //if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rg.velocity.y) < 0.05)
                //{

                //    rg.AddForce(0, jumpForce, 0);


                //}
                
            }
            else
            {

            }
            
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Meta")
            {
                if (photonView.IsMine)
                {
                   
                    photonView.RPC("Finish", RpcTarget.All);
                }
            }
            if (collision.gameObject.tag == "Fondo")
            {
                if (photonView.IsMine)
                {
                    rg.position = new Vector3(0, 0, 0);
                    rg.velocity = new Vector3(0, 0, 0);
                    
                }
            }
            
            if (collision.gameObject.tag == "Point")
            {
                if (photonView.IsMine)
                {
                    collision.gameObject.SetActive(false);
                    points += 10;
                    upointsText.text = "Puntaje: " + points;
                }
            }

        }
        [PunRPC]
        void Finish()
        {
           
            panelVictory.SetActive(true);

        }
        public void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                //rg.AddForce(new Vector3.);
                transform.Rotate(Vector3.up, x* turnspeed*Time.deltaTime);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * y);
                transform.Rotate(Vector3.up, x * turnspeed * Time.deltaTime);
                if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rg.velocity.y)<1)
                {
                    
                    rg.AddForce(0, jumpForce, 0);


                }
            }
            else
            {

            }
        }
        public virtual void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(username);
            }else if (stream.IsReading)
            {
                username=(string)stream.ReceiveNext();
                userText.text = username;
            }
        }
    }
}