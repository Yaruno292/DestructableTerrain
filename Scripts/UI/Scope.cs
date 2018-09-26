using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject scopeOverlay;

    [SerializeField]
    private Camera weaponCam;

    [SerializeField]
    private Camera mainCam;

    private float normalFov;
    private float scopedFov = 15f;

    private bool isScoped = false;

    private void Start()
    {
        weaponCam.GetComponent<Camera>();
        normalFov = mainCam.fieldOfView;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);
            if(isScoped)
            {
                StartCoroutine(TriggerScope());
            }
            else
            {
                UnScope();
            }
        }
	}

    void UnScope()
    {
        scopeOverlay.SetActive(false);
        weaponCam.enabled = true;
        mainCam.fieldOfView = normalFov;
    }

    IEnumerator TriggerScope()
    {
        yield return new WaitForSeconds(0.15f);
        scopeOverlay.SetActive(true);
        weaponCam.enabled = false;
        mainCam.fieldOfView = scopedFov;
    }
}
