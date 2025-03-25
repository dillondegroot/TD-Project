using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlacementScript : MonoBehaviour
{
    [SerializeField] GameObject circle;
    [SerializeField] GameObject tower1;

    bool isPlacing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlacing = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCircle();
        OnClick();
        PlacingToggle();
    }

    void MoveCircle()
    {
        // Move the circle to the mouse position
        Vector3 mousePos = Input.mousePosition;
        circle.transform.position = mousePos;
    }

    void PlacingToggle()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isPlacing) isPlacing = false;
            else isPlacing = true;
        }

        if (isPlacing)
        {
            circle.GetComponent<Image>().enabled = true;
        }
        else
        {
            circle.GetComponent<Image>().enabled = false;
        }
    }

    void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacing)
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);

                Physics.Raycast(ray, out RaycastHit hit, 100);

                if (hit.transform)
                {
                    GameObject tower = Instantiate(tower1);
                    tower.transform.position = new Vector3(hit.point.x, hit.point.y + 2.5f, hit.point.z);
                }
                isPlacing = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            Physics.Raycast(ray, out RaycastHit hit, 100);

            if (hit.transform)
            {
                if (hit.transform.tag == "Tower") Destroy(hit.transform.gameObject);
                isPlacing = false;
            }
        }
    }
}
