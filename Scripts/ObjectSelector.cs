using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public RectTransform crossHair;
    public Canvas canvas;
    public Camera cam;
    public bool LMBClicked=false;
    public string name;
    float pixelX;
    float pixelY;
    float posX;
    float posY;
    public static ObjectSelector instance;
    // Start is called before the first frame update
    void Start()
    {
        pixelX = canvas.worldCamera.pixelWidth;
        pixelY = canvas.worldCamera.pixelHeight;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked();
        }
    }

    void Clicked()
    {
        posX = crossHair.rect.x + pixelX / 2f;
        posY = crossHair.rect.y + pixelY / 1.5f;
        Vector3 cursorPos = new Vector3(posX, posY, 0);
        Ray ray = cam.ScreenPointToRay(cursorPos);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 20))
        {
            if (hit.transform.tag == "Pickable")
            {
                LMBClicked = true;
                //Debug.Log(hit.transform.name);
                name = hit.transform.name;
            }
        }
    }
}
