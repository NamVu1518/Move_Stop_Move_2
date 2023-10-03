using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterShow : MonoBehaviour
{

    [SerializeField] private Transform charater;

    [SerializeField] private Renderer skin;
    public Renderer Skin
    {
        set { skin = value; }
        get { return skin; }
    }
  
    [SerializeField] private Renderer pant;
    public Renderer Pant
    {
        set { pant = value; }
        get { return pant; }
    }



    [SerializeField] private float rotateSpeed;
    private float numRotate = 0f;

    void Update()
    {
        numRotate += rotateSpeed * Time.deltaTime;
        charater.rotation = Quaternion.Euler(new Vector3(charater.eulerAngles.x, numRotate, charater.eulerAngles.z));
    }
}
