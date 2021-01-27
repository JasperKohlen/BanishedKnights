﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Starts dragging blueprint once it's spawned in
public class blueprintPlacement : MonoBehaviour
{
    public static blueprintPlacement instance;
    private RaycastHit hit;
    public GameObject prefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

    }


    // Update is called once per frame
    void Update()
    {
        CarryPrefab();
        RotatePrefab();
        CarryingPrefabChecks();

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlacePrefab();
        }
    }
    private void RotatePrefab()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(0, 90, 0);
        }
    }

    private void CarryPrefab()
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
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void CarryingPrefabChecks()
    {
        if (EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
}
