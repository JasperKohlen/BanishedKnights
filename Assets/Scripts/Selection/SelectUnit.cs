using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectUnit : MonoBehaviour
{
    private SelectedUnitDictionary selectedTable;
    RaycastHit hit;
    [SerializeField] UIManager ui;

    Vector3 p1;

    // Start is called before the first frame update
    void Start()
    {
        selectedTable = GetComponent<SelectedUnitDictionary>();
    }

    // Update is called once per frame
    void Update()
    {
        //While holding leftclick
        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;
        }

        //When releasing leftclick
        if (Input.GetMouseButtonUp(0))
        {
            ManuallySelect();
        }
    }

    private void ManuallySelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(p1);

        //Casts a ray when clicking
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag.Equals("Controllable"))
        {
            //Specifically select multiple units
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (selectedTable.Contains(hit.transform.gameObject))
                {
                    selectedTable.Deselect(hit.transform.gameObject);
                }
                else
                {
                    selectedTable.AddSelected(hit.transform.gameObject);
                }
            }
            //Select only one unit, also deselecting all other units
            else
            {
                if (selectedTable.Contains(hit.transform.gameObject))
                {
                    selectedTable.DeselectAll();
                }
                else
                {
                    selectedTable.DeselectAll();
                    selectedTable.AddSelected(hit.transform.gameObject);
                    Debug.Log("Unit selected");

                }
            }
        }
        else
        {
            selectedTable.DeselectAll();
        }
        //When clicking on a storagebuilding
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag.Equals("Storage"))
        {
            Debug.Log("Storage selected");
            ui.OpenStorageMenu(hit.transform.GetComponent<LocalStorageDictionary>());
        }
        //When you dont click on a unit
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            ui.CloseStorageMenu();
        }
    }
}