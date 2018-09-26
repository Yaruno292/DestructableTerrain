using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    private PlayerShoot shoot;

    private void Start()
    {
        shoot = GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update () {

        //Handgun
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shoot.SwitchWeapon(0,15,false,1,0.3f,30, 100f, 230f);
        }

        //M4
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shoot.SwitchWeapon(1,45,true,2,0.1f,10, 150f, 240f);
        }

        //Shotty
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shoot.SwitchWeapon(2,5,false,2,0.8f,60, 50f, 280f);
        }

        //AK
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            shoot.SwitchWeapon(3,30,true,2,0.2f,20, 120f, 240f);
        }

        //Sniper
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            shoot.SwitchWeapon(4,6,false,3,1f,90, 300f, 300f);
        }

        /*
        //RPG
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            shoot.SwitchWeapon(5,1,false,6,0f,90, 100f, 320f);
        }

        //DubstepGun
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            shoot.SwitchWeapon(6,100,true,3,0.8f,40, 75f, 240f);
        }
        */
    }
}
