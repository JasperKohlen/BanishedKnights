using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedUnitDictionary : MonoBehaviour
{
    private Dictionary<int, GameObject> selectedUnits = new Dictionary<int, GameObject>();
    
    public void AddSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!selectedUnits.ContainsKey(id))
        {
            selectedUnits.Add(id, go);
            go.AddComponent<SelectionComponent>();
        }
    }

    public void Deselect(GameObject go)
    {
        int id = go.GetInstanceID();
        if (selectedUnits.ContainsKey(id))
        {
            Destroy(selectedUnits[id].GetComponent<SelectionComponent>());
            selectedUnits.Remove(id);
        }
    }

    public void DeselectAll()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            foreach (KeyValuePair<int, GameObject> pair in selectedUnits)
            {
                //Check if object has not been destroyed yet
                if (pair.Value != null)
                {
                    Destroy(selectedUnits[pair.Key].GetComponent<SelectionComponent>());
                }
            }
            //Table needs to be cleared on top of destroy selectioncomponents
            selectedUnits.Clear();
        }
    }

    public bool Contains(GameObject go)
    {
        if (selectedUnits.ContainsKey(go.GetInstanceID()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ContainsAnyControllables()
    {
        if (selectedUnits.Any(item => item.Value.gameObject.tag.Equals("Controllable")))
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
        return selectedUnits;
    }
}
