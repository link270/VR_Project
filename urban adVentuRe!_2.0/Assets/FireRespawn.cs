using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class FireRespawn : MonoBehaviour
    {

        public GameObject respawn;
        public Player player;
        private PlayerController controller;
        private bool isTriggered = false;




        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == 8 && !isTriggered){
                isTriggered = true;
                StartCoroutine(RespawnPlayer());
            }
        }

        IEnumerator RespawnPlayer(){
            gameObject.GetComponent<AudioSource>().Play();            
            controller.isTeleporting = true;
            SteamVR_Fade.Start(Color.red, 0.2f);
            yield return new WaitForSeconds(0.2f);

            Vector3 translation = respawn.transform.position - player.transform.position;
            player.transform.Translate(translation.x, translation.y, translation.z, Space.World);



            SteamVR_Fade.Start(Color.clear, 0.2f);
            yield return new WaitForSeconds(0.2f);
            isTriggered = false;
            controller.isTeleporting = false;
        }

        void Start()
        {
            player = Player.instance;
            controller = player.GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}