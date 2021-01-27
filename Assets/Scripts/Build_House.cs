using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_House : MonoBehaviour
{
    public GameObject houseBlueprint;
    public GameObject storageBlueprint;

    public void SpawnHouseBlueprint()
    {
        Instantiate(houseBlueprint);
    }

    public void SpawnStorageBlueprint()
    {
        Instantiate(storageBlueprint);
    }
}
