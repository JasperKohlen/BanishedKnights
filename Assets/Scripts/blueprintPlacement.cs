using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprintPlacement : MonoBehaviour
{
    private RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        MovePrefab();
        RotatePrefab();
        PlacePrefab();
    }
    private void RotatePrefab()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(0, 45, 0);
        }
    }

    private void MovePrefab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Cast a ray, when it hits a collider -> return hitinfo
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
            //Adjust building's rotation based on terrain
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void PlacePrefab()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
