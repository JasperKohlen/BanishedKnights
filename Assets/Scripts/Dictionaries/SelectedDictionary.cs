using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedDictionary : MonoBehaviour, IDictionary
{
    private Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void Add(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!selectedTable.ContainsKey(id))
        {
            selectedTable.Add(id, go);
            go.AddComponent<SelectionComponent>();
        }
    }

    public void Remove(GameObject go)
    {
        int id = go.GetInstanceID();
        if (selectedTable.ContainsKey(id))
        {
            Destroy(selectedTable[id].GetComponent<SelectionComponent>());
            selectedTable.Remove(id);
        }
    }

    public void DeselectAll()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            foreach (KeyValuePair<int, GameObject> pair in selectedTable)
            {
                //Check if object has not been destroyed yet
                if (pair.Value != null)
                {
                    Destroy(selectedTable[pair.Key].GetComponent<SelectionComponent>());
                }
            }
            //Table needs to be cleared on top of destroy selectioncomponents
            selectedTable.Clear();
        }
    }

    public bool Contains(GameObject go)
    {
        if (selectedTable.ContainsKey(go.GetInstanceID()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Dictionary<int, GameObject> GetTable()
    {
        return selectedTable;
    }
}
