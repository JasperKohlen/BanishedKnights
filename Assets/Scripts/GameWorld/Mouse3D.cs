using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour
{
    public static Mouse3D Instance { get; private set; }
    [SerializeField] private LayerMask mouseColliderMask = new LayerMask();
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mouseColliderMask))
        {
            transform.position = hit.point;
        }
    }

    public static Vector3 GetMouseWorldPosition3D() => Instance.GetMouseWorldPosition3D_Instance();
    private Vector3 GetMouseWorldPosition3D_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, mouseColliderMask))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
