using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemovableSelection : MonoBehaviour
{
    SelectedDictionary selectedTable;
    RaycastHit hit;

    bool dragSelect;

    MeshCollider selectionBox;
    Mesh selectionMesh;

    //The corners of our 2d selectionbox
    Vector2[] corners;

    //The vertices of our meshcollider
    Vector3[] verts;
    Vector3[] vecs;

    Vector3 p1;
    Vector3 p2;
    // Start is called before the first frame update
    void Start()
    {
        selectedTable = GetComponent<SelectedDictionary>();
        dragSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        //While holding leftclick
        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;
        }

        //While dragging and holding
        if (Input.GetMouseButton(0))
        {
            //Confirm that we are not just clicking but actually dragging
            if ((p1 - Input.mousePosition).magnitude > 40)
            {
                dragSelect = true;
            }

        }

        //When releasing leftclick
        if (Input.GetMouseButtonUp(0))
        {
            //Select single unit(s)
            if (dragSelect == false)
            {
                ManuallySelect();
            }
            //Draw selectionbox while dragging
            else
            {
                DrawSelectionBox();
            }
            dragSelect = false;
        }
    }

    private void ManuallySelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(p1);

        //When clicking on a removable object
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Removable")
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
            //Select only one unit, therefore also deselecting all other units
            else
            {
                if (selectedTable.Contains(hit.transform.gameObject))
                {
                    selectedTable.DeselectAll();
                }
                else
                {
                    //TODO: small bug when multiple are selecting and you then try to only select 1 tree who was already selected
                    //The tree you click on doesnt get selected until you click again afterwards
                    selectedTable.DeselectAll();
                    selectedTable.AddSelected(hit.transform.gameObject);
                }
            }
        }
        //When you dont click on a removable object 
        else 
        {
            selectedTable.DeselectAll();
        }
    }

    private void DrawSelectionBox()
    {
        verts = new Vector3[4];
        vecs = new Vector3[4];
        int i = 0;
        p2 = Input.mousePosition;
        corners = getBoundingBox(p1, p2);

        //Perform raycast from screenposition into the world to select multiple objects
        foreach (Vector2 corner in corners)
        {
            Ray ray = Camera.main.ScreenPointToRay(corner);

            //Add each of the lines that intersect with the ground by the raycast to the verts
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Removable")
            {
                verts[i] = new Vector3(hit.point.x, 0, hit.point.z);
                vecs[i] = ray.origin - hit.point;
                Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
            }
            i++;
        }

        //Draw the boxmesh based on the verts generated in the foreach loop
        selectionMesh = generateSelectionMesh(verts, vecs);

        selectionBox = gameObject.AddComponent<MeshCollider>();
        selectionBox.sharedMesh = selectionMesh;
        selectionBox.convex = true;
        selectionBox.isTrigger = true;

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            selectedTable.DeselectAll();
        }

        Destroy(selectionBox, 0.02f);
    }

    private void OnGUI()
    {
        if (dragSelect == true)
        {
            //Draw rectangle
            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    //Algorithm that ensures the box corners are correctly alligned
    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2)
    {
        Vector2 newP1;
        Vector2 newP2;
        Vector2 newP3;
        Vector2 newP4;

        if (p1.x < p2.x) //if p1 is to the left of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = p1;
                newP2 = new Vector2(p2.x, p1.y);
                newP3 = new Vector2(p1.x, p2.y);
                newP4 = p2;
            }
            else //if p1 is below p2
            {
                newP1 = new Vector2(p1.x, p2.y);
                newP2 = p2;
                newP3 = p1;
                newP4 = new Vector2(p2.x, p1.y);
            }
        }
        else //if p1 is to the right of p2
        {
            if (p1.y > p2.y) // if p1 is above p2
            {
                newP1 = new Vector2(p2.x, p1.y);
                newP2 = p1;
                newP3 = p2;
                newP4 = new Vector2(p1.x, p2.y);
            }
            else //if p1 is below p2
            {
                newP1 = p2;
                newP2 = new Vector2(p1.x, p2.y);
                newP3 = new Vector2(p2.x, p1.y);
                newP4 = p1;
            }

        }

        Vector2[] corners = { newP1, newP2, newP3, newP4 };
        return corners;

    }

    //generate a mesh from the 4 bottom points
    //The world is 3D but the box is 2D, so the code generates a box from the screen position rectangle into the game (bottom rectangle)
    Mesh generateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        //Bottom rectangle
        for (int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        //Top rectangle
        for (int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        selectedTable.AddSelected(other.gameObject);
    }
}
