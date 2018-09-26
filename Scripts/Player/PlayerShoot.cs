using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    [SerializeField]
    private Camera fpsCam;

    public int currentAmmo;
    public int maxAmmo;
    private int currentWeapon = 0;
    private int currentReloadTime;
    private float currentWeaponTbb;
    private bool automatic;
    private float range;
    private int damage;
    private float impactForce;

    private bool hold = false;

    private float normalFov;
    private float scopedFov = 15f;

    private bool isScoped = false;


    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private ParticleSystem[] particles;
    [SerializeField]
    private GameObject impactParticle;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] ShotSounds;
    [SerializeField]
    private AudioClip[] ReloadSounds;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject scopeOverlay;

    [SerializeField]
    private Camera weaponCam;

    [SerializeField]
    private Camera mainCam;


    // Use this for initialization
    void Start () {

        audioSource = fpsCam.GetComponent<AudioSource>();

        weaponCam.GetComponent<Camera>();
        normalFov = mainCam.fieldOfView;

        for (int i = 1; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        SwitchWeapon(0, 15, false, 1, 0.3f, 30, 100f, 230f);
    }

    public void SwitchWeapon(int weaponNum, int weaponAmmo, bool autoWeapon, int reloadTime, float tbb, int dmg, float weaponRange, float weaponForce)//tbb = time between bullets
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        currentWeapon = weaponNum;
        weapons[weaponNum].SetActive(true);
        maxAmmo = weaponAmmo;
        currentAmmo = weaponAmmo;
        automatic = autoWeapon;
        currentReloadTime = reloadTime;
        currentWeaponTbb = tbb;
        range = weaponRange;
        damage = dmg;
        impactForce = weaponForce;
    }

    // Update is called once per frame
    void Update()
    {
        //TriggerFinger
        if (Input.GetKeyDown(KeyCode.Mouse0) && automatic == false && hold == false) {
            if (currentAmmo > 0)
            {
                Shoot();
                hold = true;
            }
            else if (currentAmmo <= 0)
            {
                Reload();
            }
        }

        //Automatic
        if (Input.GetKey(KeyCode.Mouse0) && automatic == true && hold == false)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                hold = true;
            }
            else if (currentAmmo <= 0)
            {
                Reload();
            }
        }

        //Reload
        if (Input.GetKeyDown(KeyCode.R) && hold == false)
        {
            Reload();
        }

        //Scope
        if (Input.GetKeyDown(KeyCode.Mouse1) && currentWeapon == 4)
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);
            if (isScoped)
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

    IEnumerator ReloadTime(int reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        hold = false;
    }

    IEnumerator TBB(float weaponTbb)
    {
        currentAmmo -= 1;
        yield return new WaitForSeconds(weaponTbb);
        hold = false;
    }

    private void Reload()
    {
        audioSource.clip = ReloadSounds[currentWeapon];
        audioSource.Play();
        hold = true;
        StartCoroutine(ReloadTime(currentReloadTime));
    }

    private void Shoot()
    {
        StartCoroutine(TBB(currentWeaponTbb));
        particles[currentWeapon].Play();
        audioSource.clip = ShotSounds[currentWeapon];
        audioSource.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDmg(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
        GameObject impactObj = Instantiate(impactParticle, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactObj, 2f);
        weapons[currentWeapon].GetComponent<Animation>().Play();
    }
}
