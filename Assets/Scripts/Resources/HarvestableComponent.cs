using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableComponent : MonoBehaviour
{
    public GameObject resource;
    [HideInInspector] public bool harvested;
    [HideInInspector] public bool isTarget;
}
