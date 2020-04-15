using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
        public class IndianaReset : MonoBehaviour
    {
        // Start is called before the first frame update
        private GameObject [] restores;
        private GameObject playerRestore; 
        private GameObject[] statues;
        private Player player;
        private GameObject ball;
        private GameObject ballRestore;
        void Start()
        {
            player = Player.instance;
            restores = GameObject.FindGameObjectsWithTag("IndianaRestores");
            statues = GameObject.FindGameObjectsWithTag("IndianaStatues");
            playerRestore = GameObject.Find("PlayerRestore");
            ball = GameObject.Find("IndianaBall");
            ballRestore = GameObject.Find("IndianaBallRestore");
        }

        public IEnumerator ResetIndiana(){
            //Fade player's vision to black
            SteamVR_Fade.Start(Color.black, 0.2f);
            //Move the player
            Vector3 translation = playerRestore.transform.position - player.transform.position;
            player.transform.Translate(translation.x, translation.y, translation.z, Space.World);


            //Reset the statues
            for (int i = 0; i < restores.Length; ++i){
                statues[i].transform.position = restores[i].transform.position;
            }

            //Reset the ball
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.useGravity = false;
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ball.transform.position = ballRestore.transform.position;

            //Restore player's vision
            yield return new WaitForSeconds(0.2f);
            SteamVR_Fade.Start(Color.clear, 0.2f);
            yield return new WaitForSeconds(0.2f);
            
            //Reset the ball again just in case
            ballRigidbody.useGravity = false;
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            ball.transform.position = ballRestore.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}