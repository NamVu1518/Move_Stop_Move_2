using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShowCamera : MonoBehaviour
{
    [SerializeField] private Transform uiWeaponRotate;
    [SerializeField] private GameObject rawWeapon;
    private float numRotateSpeed = 50;
    private float numRotate = 0;

    public GameObject RawWeapon
    {
        get { return rawWeapon; }
    }

    private void Update()
    {
        numRotate += numRotateSpeed * Time.deltaTime;
        uiWeaponRotate.localRotation = Quaternion.Euler(new Vector3(uiWeaponRotate.eulerAngles.x, numRotate, uiWeaponRotate.eulerAngles.z));
    }
}
