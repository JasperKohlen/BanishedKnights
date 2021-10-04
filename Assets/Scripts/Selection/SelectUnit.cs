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
            if (selectedTable.Contains(hit.transform.gameObject))
            {
                selectedTable.Remove(hit.transform.gameObject);
            }
            else
            {
                selectedTable.Add(hit.transform.gameObject);
            }
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            selectedTable.DeselectAll();
        }
        //When clicking on a storagebuilding
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag.Equals("Storage"))
        {
            Debug.Log("Storage selected");
            hit.transform.gameObject.GetComponent<LocalStorageDictionary>().isSelected = true;
            ui.OpenStorageMenu(hit.transform.GetComponent<LocalStorageDictionary>());
        }
        else
        {
            ui.CloseStorageMenu();

            //foreach (var item in FindObjectsOfType<LocalStorageDictionary>())
            //{
            //    item.isSelected = false;
            //}
        }
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag.Equals("Barracks"))
        {
            Debug.Log("Barracks selected");
            hit.transform.gameObject.GetComponent<LocalStorageDictionary>().isSelected = true;
            ui.OpenBarracksMenu(hit.transform.GetComponent<LocalStorageDictionary>());
        }
        else if (!EventSystem.current.IsPointerOverGameObject())
        {
            ui.CloseBarracksMenu();

            //foreach (var item in FindObjectsOfType<LocalStorageDictionary>())
            //{
            //    item.isSelected = false;
            //}
        }
    }
}
