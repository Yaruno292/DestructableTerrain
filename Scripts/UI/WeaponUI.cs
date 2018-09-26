using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

    private GameObject ammoUIobj;
    private Text ammoUItext;

    private PlayerShoot shoot;

	// Use this for initialization
	void Start () {
        ammoUIobj = GameObject.Find("AmmoUI");
        ammoUItext = ammoUIobj.GetComponent<Text>();
        shoot = GetComponent<PlayerShoot>();
	}
	
	// Update is called once per frame
	void Update () {
        ammoUItext.text = shoot.currentAmmo + "/" + shoot.maxAmmo + " ";
	}
}
