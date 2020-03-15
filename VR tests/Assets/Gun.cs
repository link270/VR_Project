using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{

    public GameObject bullet;
    public Transform barrelPivot;
    public float shootingSpeed = 1;
    //public GameObject muzzleFlash;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        Debug.Log("Fire");
        Rigidbody bulletrb = Instantiate(bullet, barrelPivot.position, barrelPivot.rotation).GetComponent<Rigidbody>();
        bulletrb.velocity = barrelPivot.forward * shootingSpeed;
        //muzzleFlash.SetActive(true);
    }
}
