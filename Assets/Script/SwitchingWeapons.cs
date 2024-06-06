using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingWeapons : MonoBehaviour
{
    public int weaponSelected = 0;

    private void Start()
    {
        SelectedWeapon();
    }



    private void Update()
    {
        int previousWeaponSelected = weaponSelected;
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            weaponSelected = 0;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            weaponSelected = 1;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            weaponSelected = 2;
        }
        if(weaponSelected != previousWeaponSelected)
        {
            SelectedWeapon();
        }
    }

    void SelectedWeapon()
    {
        int i = 0;
        foreach (Transform Gun in transform)
        {
            Gun.gameObject.SetActive(i == weaponSelected);
            i++;
        }
    }
}
