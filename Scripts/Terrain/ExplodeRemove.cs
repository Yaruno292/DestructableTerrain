using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeRemove : MonoBehaviour {

    float i = 2.5f;
    
	// Update is called once per frame
	void Update () {
		if(i <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            i -= Time.deltaTime;
        }
	}
}
