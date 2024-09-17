using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GunController : MonoBehaviour
{
    public Transform Player; 
    public Animator CameraAnim;

    public Animator PlayerAnimator;

    public ParticleSystem bulletDebris;
    public ParticleSystem bloodSpray;

    private GunType gun;
    public Transform Body;
    public Transform MagHolder;

    private bool reloading;

    public float cooldown = 0;
    public float angle; // gun angle / could be used later

    void Awake(){
        gun = GetComponentInChildren<GunType>();
    }

    public void SwitchGun(int gunChoice){
        switch (gunChoice){
            case 1:
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gun = GetComponentInChildren<GunType>();
                PlayerAnimator.SetTrigger("Rifle");
            break;

            case 2:
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gun = GetComponentInChildren<GunType>();
                PlayerAnimator.SetTrigger("Pistol");
            break;
        }
    }

    public void ShootGun(){
        cooldown = gun.FireRate;
        gun.Magazine -= 1;

        RaycastHit2D hit = Physics2D.Raycast(gun.transform.position, transform.right, 100, LayerMask.GetMask("Environment", "Enemies"));

        CameraAnim.SetTrigger("Shot");
        gun.Animator.SetTrigger("Shot");

        foreach (var fx in gun.AmmoEffects){
            fx.Play();
        }

        if (hit.collider){
            ParticleSystem shotDebris;
            switch(hit.transform.gameObject.layer){
                case 7:
                    EnemyStats enemy = hit.transform.GetComponentInParent<EnemyStats>();

                    enemy.Health -= gun.Damage;
                    hit.rigidbody.AddForce((hit.transform.position-transform.position).normalized * 2.2f, ForceMode2D.Impulse);

                    shotDebris = Instantiate(bloodSpray, hit.transform.position, Quaternion.Euler(0, 0, 0));
                    Destroy(shotDebris.gameObject, 2);
                break;

                case 6:
                    shotDebris = Instantiate(bulletDebris, hit.point, Quaternion.Euler(0, 0, 0));
                    Destroy(shotDebris.gameObject, 5);
                    Destroy(shotDebris.transform.GetChild(0).gameObject, 5);
                break;
            }
        }
    }

    public void GrabMag(){
        gun.GrabMag(MagHolder);
    }

    public void InsertMag(){
        gun.InsertMag(gun.transform);
    }

    void Update() {
        MoveGun();

        if (cooldown > 0){
            cooldown -= 1 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            SwitchGun(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)){
            SwitchGun(2);
        }

        if (Input.GetKeyDown(KeyCode.R) && cooldown <= 0){
            if (gun.Magazine < gun.MaxMagazine){
                PlayerAnimator.SetTrigger("Reload");

                cooldown = gun.ReloadTime;
                gun.Magazine = gun.MaxMagazine;
            }
        }

        if (gun.Magazine > 0 && cooldown <= 0 && !reloading){
            if (gun.Automatic){
                if (Input.GetMouseButton(0)){
                    ShootGun();
                }
            } else {
                if (Input.GetMouseButtonDown(0)){
                    ShootGun();
                }
            }
        }

        // DEBUG
        //Debug.DrawRay(gun.transform.position, transform.right * 100f, Color.magenta);
    }


    void MoveGun(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; 

        Vector3 direction = (mousePos - Player.position).normalized;

        transform.position = Player.position + new Vector3(0, gun.HoldOffset.y);

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mousePos.x < Player.transform.position.x){
            gun.transform.localScale = new Vector2(-1, -1);
            Body.transform.localScale = new Vector2(-1, 1);
        } else {
            gun.transform.localScale = new Vector2(-1, 1);
            Body.transform.localScale = new Vector2(1, 1);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
