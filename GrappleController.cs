
using UnityEngine;

public class GrappleController : MonoBehaviour 
{
    public GameObject gPointPrefab;
    public GameObject mouseSprite;

    private bool grappleActive = false;

    public DistanceJoint2D rope;

    private GameObject grapplePoint;

    private Transform Gun;
    public Transform GunCont;

    void Start(){
        Gun = GunCont.GetChild(0);
    }

    void Update(){
        Debug.DrawRay(Gun.transform.position, GunCont.transform.right * 10f, Color.green);
        if (Input.GetMouseButtonDown(1)){
            switch (grappleActive){
                case false:
                    CreatePoint();
                break;
                
                case true:
                    DestroyPoint();
                break;
            }
        }
    }

    void CreatePoint(){
        Gun = GunCont.GetChild(0);
        RaycastHit2D hit = Physics2D.Raycast(Gun.transform.position, GunCont.transform.right, 10f, LayerMask.GetMask("Environment"));

        if (hit){
            grapplePoint = Instantiate(gPointPrefab);
            grapplePoint.transform.position = hit.point;

            rope.enabled = true;
            rope.connectedBody = grapplePoint.GetComponent<Rigidbody2D>();

            mouseSprite.SetActive(false);

            grappleActive = true;
        }
    }

    void DestroyPoint(){
        rope.connectedBody = null;
        rope.enabled = false;

        grappleActive = false;

        Destroy(grapplePoint);

        mouseSprite.SetActive(true);
    }
}
