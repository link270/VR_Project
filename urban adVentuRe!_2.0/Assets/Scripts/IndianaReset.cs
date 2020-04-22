using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
        public class IndianaReset : MonoBehaviour
    {
        public AudioSource sound;
        public GameObject solver;
        // Start is called before the first frame update
        private GameObject [] restores;
        private GameObject playerRestore; 
        private GameObject[] statues;
        private Player player;
        private PlayerController controller;
        private GameObject ball;
        private GameObject ballRestore;
        void Start()
        {
            player = Player.instance;
            controller = player.GetComponent<PlayerController>();
            restores = GameObject.FindGameObjectsWithTag("IndianaRestores");
            statues = GameObject.FindGameObjectsWithTag("IndianaStatues");
            playerRestore = GameObject.Find("PlayerRestore");
            ball = GameObject.Find("IndianaBall");
            ballRestore = GameObject.Find("IndianaBallRestore");
            sound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
            solver = GameObject.Find("TrapFloorForBall");
        }

        public IEnumerator ResetIndiana(){
            //Fade player's vision to black
            controller.isTeleporting = true;
            SteamVR_Fade.Start(Color.red, 0.2f);
            sound.Play();
            solver.GetComponent<RetractIfStatuesRemoved>().isActive = false;
            //Move the player
            foreach(GameObject statue in statues){
                statue.GetComponent<SimpleAttach>().DetachSelfFromHand();
            }

            Vector3 translation = playerRestore.transform.position - player.transform.position;
            player.transform.Translate(translation.x, translation.y, translation.z, Space.World);


            //Reset the statues
            Rigidbody rigidbody;
            for (int i = 0; i < restores.Length; ++i){
                rigidbody = statues[i].GetComponent<Rigidbody>();
                statues[i].transform.rotation = Quaternion.Euler(new Vector3(-90,0,-90));
                statues[i].transform.position = restores[i].transform.position;
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }

            //Reset the ball
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.useGravity = false;
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ball.transform.position = ballRestore.transform.position;


            
            yield return new WaitForSeconds(1);
            //Reset the ball again just in case
            ballRigidbody.useGravity = false;
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ball.transform.position = ballRestore.transform.position;
            ball.GetComponent<AudioSource>().Stop();

            controller.isTeleporting = false;
            solver.GetComponent<RetractIfStatuesRemoved>().isActive = true;
            solver.GetComponent<RetractIfStatuesRemoved>().ballRolling = false;
            
            SteamVR_Fade.Start(Color.clear, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}