using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NadeExplode : MonoBehaviour {

    private TerrainDestruction terrainD;

    private float offTime = 3f;
    private float raduis = 8f;
    private float force = 800f;
    private float damage = 300;
    private bool hasExploded = false;

    [SerializeField]
    private GameObject explosion;

    // Use this for initialization
    void Awake()
    {
        terrainD = GetComponent<TerrainDestruction>();
    }


    void Update()
    {
        if(offTime <= 0 && hasExploded == false)
        {
            Debug.Log("Boom");
            hasExploded = true;
            Explode();
        }
        else if(hasExploded == false)
        {
            offTime -= Time.deltaTime;
        }
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, raduis);

        foreach (Collider nearObj in collidersToDestroy)
        {
            Target target = nearObj.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDmg(damage);
            }
        }

        foreach (Collider nearTerrain in collidersToDestroy)
        {
            Terrain terrain = nearTerrain.GetComponent<Terrain>();
            if (terrain != null)
            {
                terrainD.DestroyTerrain();
            }
            else
            {
                Destroy(gameObject);
            }
        }


        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, raduis);

        foreach (Collider nearObj in collidersToMove)
        {
            Rigidbody rb = nearObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, raduis);
            }
        }
    }
}
