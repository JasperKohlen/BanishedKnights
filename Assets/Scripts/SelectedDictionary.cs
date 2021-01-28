using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void AddSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!selectedTable.ContainsKey(id))
        {
            selectedTable.Add(id, go);
            go.AddComponent<SelectionComponent>();
        }
    }

    public void Deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<SelectionComponent>());
        selectedTable.Remove(id);
    }

    public void DeselectAll()
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
