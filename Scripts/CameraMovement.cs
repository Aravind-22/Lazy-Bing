using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target,player,torch,torchLightTarget,obstruction;
    public float rotateSpeed = 15,zoomSpeed;
    AudioSource backgroundAudio;
    float mouseX,mouseY;
    Vector3 moveY;
    public static CameraMovement instance;
    void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        obstruction = target;
        backgroundAudio = GetComponent<AudioSource>();
    }
    void LateUpdate()
    {
        if (GameManager.instance.onPause == false)
        {
            MouseControl();
            ViewObstructed();
        }
    }
    void MouseControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotateSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotateSpeed;
        mouseY = Mathf.Clamp(mouseY, -10, 40f);
        //Debug.Log(mouseY);
        transform.LookAt(target);
        torch.LookAt(torchLightTarget);
        moveY = torchLightTarget.transform.position;
        if (mouseY > 25)
        {            
            moveY.y = player.position.y + 0.90f;
        }
        else if (mouseY < 5)
        {
            moveY.y = player.position.y + 2.30f;
        }
        else
        {
            moveY.y = player.position.y + 1.60f;
        }
        torchLightTarget.transform.position = moveY;
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    void ViewObstructed()
    {
        RaycastHit hit;
        obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        if (Physics.Raycast(transform.position,target.position-transform.position,out hit,2f))
        {
            if (hit.collider.tag == "Wall")
            {
                obstruction = hit.transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
            else
            {
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }

    public void MuteGame()
    {
        backgroundAudio.enabled = false;
        GameManager.instance.atmos.enabled = false;
        PlayerMovement.instance.playerWalk.enabled = false;
    }
    public void UnMuteGame()
    {
        backgroundAudio.enabled = true;
        GameManager.instance.atmos.enabled = true;
        PlayerMovement.instance.playerWalk.enabled = true;
    }
    
}
