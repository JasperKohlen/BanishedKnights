using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settler : MonoBehaviour
{
    [SerializeField] private GameObject housePrefab;
    [SerializeField] private GameObject storagePrefab;
    [SerializeField] private GameObject workerPrefab;

    Vector3 house1Pos;
    Vector3 house2Pos;

    Vector3 storagePos;

    Vector3 worker1Pos;
    Vector3 worker2Pos;
    Vector3 worker3Pos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ConsumeSettler();
        }
    }

    public void ConsumeSettler()
    {
        DeterminePrefabPositions();

        Instantiate(housePrefab, house1Pos, Quaternion.identity);
        Instantiate(housePrefab, house2Pos, Quaternion.identity);
        Instantiate(storagePrefab, storagePos, Quaternion.identity);
        Instantiate(workerPrefab, worker1Pos, Quaternion.identity);
        Instantiate(workerPrefab, worker2Pos, Quaternion.identity);
        Instantiate(workerPrefab, worker3Pos, Quaternion.identity);

        Destroy(gameObject);
    }

    private void DeterminePrefabPositions()
    {
        house1Pos = new Vector3(gameObject.transform.position.x + 20, gameObject.transform.position.y, gameObject.transform.position.z);
        house2Pos = new Vector3(gameObject.transform.position.x - 20, gameObject.transform.position.y, gameObject.transform.position.z);

        storagePos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 20);

        worker1Pos = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
        worker2Pos = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, gameObject.transform.position.z - 1);
        worker3Pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 2);

    }
}
