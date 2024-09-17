using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10f;
    //private float velocity = 0f;

    private bool grappling;
    public bool grounded;

    public Rigidbody2D rb;
    private GunController GunCont;
    private BodyController BodyCont;

    void Start(){
        GunCont = GetComponentInChildren<GunController>();
        BodyCont = GetComponentInChildren<BodyController>();
    }

    public void GrabMag(){
        GunCont.GrabMag();
    }

    public void InsertMag(){
        GunCont.InsertMag();
    }
    
    void Update(){
        float Xmov = Input.GetAxis("Horizontal");
        float Ymov = Input.GetAxis("Vertical");

        Vector2 InputVector = new Vector2(Xmov, 0);

        if (Physics2D.Raycast(transform.position - new Vector3(0f, 1.05f, 0), Vector3.down, 0.8f, LayerMask.GetMask("Environment"))){
            grounded = true;
        } else {
            grounded = false;
        }

        if (rb.velocity.y < -12){
            InputVector = new Vector2(Xmov * 0.075f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded){

            rb.AddForce(Vector2.up * 25f, ForceMode2D.Impulse);
        }

        transform.Translate(speed * Time.deltaTime * InputVector);
    }
}
