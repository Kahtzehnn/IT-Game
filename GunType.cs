using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunType : MonoBehaviour
{
    // Define Values
    public int Magazine;
    public int MaxMagazine;
    public float ReloadTime;

    public GameObject MagObject;

    public float FireRate;
    public bool Automatic;

    public int Damage;

    public Vector2 HoldOffset;
    public Vector2 MagOffset;

    public Animator Animator;

    public ParticleSystem[] AmmoEffects;

    public void GrabMag(Transform magHolder){
        MagObject.transform.SetParent(magHolder);
        MagObject.transform.localPosition = MagOffset;
    }

    public void InsertMag(Transform parent){
        MagObject.transform.SetParent(parent);
        MagObject.transform.localScale = new Vector3(1, 1);
        MagObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0), Quaternion.Euler(new Vector3(0, 0)));
    }
}
