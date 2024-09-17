using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public Transform  ArmB;
    public Transform  ArmF;

    public Transform Torso;

    public GameObject GunCont;
    private GunType gun;

    private float angle;

    void Awake(){

    }

    void Update(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; 

        Quaternion ArmRot;

        gun = GunCont.GetComponentInChildren<GunType>();

        Vector3 direction = (mousePos - gun.transform.position).normalized;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (mousePos.x < transform.position.x){
            ArmRot = Quaternion.Euler(0, 0, angle + 180);
        } else {
            ArmRot = Quaternion.Euler(0, 0, angle);
        }

        ArmB.rotation = ArmRot;
        ArmF.rotation = ArmRot;

        float normalizedAngle = NormalizeAngle(angle + 90);

        if (mousePos.x < transform.position.x){
            Torso.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(normalizedAngle, -120f, -30f));
        } else {
            Torso.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(normalizedAngle, 30f, 120f));
        }
    }
    
    float NormalizeAngle(float angle) {
        angle = angle % 360;
        if (angle > 180) angle -= 360;
        else if (angle < -180) angle += 360;
        return angle;
    }
}