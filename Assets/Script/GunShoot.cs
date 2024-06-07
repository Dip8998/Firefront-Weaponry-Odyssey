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
    public AudioClip reloadClip;
    private float nextFireTiming = 0f;
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator reloadAnim;
   

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();

        currentAmmo = maxAmmo;
    }
    private void OnEnable()
    {
        isReloading = false;
        reloadAnim.SetBool("Reloading", false);
    }
    private void Update()
    {
        if (isReloading)
        return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >= nextFireTiming)
        {
            nextFireTiming = Time.time + 1f / fireRate;
            myAudio.clip = gunSound;
            myAudio.Play();
            Shoot();
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");
        reloadAnim.SetBool("Reloading", true);
        yield return new WaitForSeconds(1f);
        myAudio.clip = reloadClip;
        myAudio.Play();
        yield return new WaitForSeconds(reloadTime - 0.25f);
       
        reloadAnim.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void Shoot()
    {
        currentAmmo--;
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
