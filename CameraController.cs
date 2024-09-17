using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cursorSprite;
    public float lerpSpeed;  // Speed of the camera movement
    public Transform player;
    public Transform GunHolder;

    void Start(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        Vector3 offset = (mousePosition - player.position) / 3.2f;

        Vector3 targetPosition = player.position + offset;

        targetPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

        cursorSprite.transform.position = mousePosition;
    }
}
