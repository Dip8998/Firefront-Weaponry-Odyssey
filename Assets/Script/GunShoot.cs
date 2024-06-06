using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float hitForce = 30f;
    public Camera cam;
    public GameObject particalEffect;
    //public GameObject ImpactEffect;
    public float fireRate = 10f;
    private AudioSource myAudio;
    public AudioClip gunSound;
    private float nextFireTiming = 0f;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextFireTiming)
        {
            nextFireTiming = Time.time + 1f / fireRate;
            myAudio.clip = gunSound;
            myAudio.Play();
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Takedamge(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
            var impactEffectIstance = Instantiate(particalEffect, transform.position, transform.rotation) as GameObject;

            Destroy(impactEffectIstance, 4);
          // GameObject obj = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //obj.transform.LookAt(hit.point + hit.normal);
        }
    }
}
