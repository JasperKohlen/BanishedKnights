using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuilding : MonoBehaviour
{
    RaycastHit hit;
    Vector3 p1;
    [SerializeField] UIManager ui;

    // Update is called once per frame
    void Update()
    {
        //When releasing leftclick
        if (Input.GetMouseButtonUp(0))
        {
            p1 = Input.mousePosition;
            Select();
        }
    }

    private void Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(p1);

        //When clicking on a removable object
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag.Equals("Storage"))
        {
            Debug.Log("Storage clicked");
            ui.OpenStorageMenu(hit.transform.GetComponent<LocalStorageDictionary>());
        }
        else
        {
            ui.CloseStorageMenu();
        }
    }
}
