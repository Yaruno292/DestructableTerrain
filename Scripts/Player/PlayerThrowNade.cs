using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowNade : MonoBehaviour {

    [SerializeField]
    private GameObject nade;
    private bool throwable = true;
    private float tbn; //Time between nades

    [SerializeField]
    private float force = 10f;

    [SerializeField]
    private float defaultTbn = 1f;

    void Start()
    {
        tbn = defaultTbn;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q) && throwable)
        {
            throwable = false;
            GameObject grenade = Instantiate(nade,transform.position,transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward*force, ForceMode.VelocityChange);
        }

        if (tbn <= 0)
        {
            throwable = true;
            tbn = defaultTbn;
        }
        else if(throwable == false)
        {
            tbn -= Time.deltaTime;
        }
    }
}
