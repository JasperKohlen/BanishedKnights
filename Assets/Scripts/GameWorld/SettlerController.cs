using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlerController : MonoBehaviour
{
    private List<Settler> settlers = new List<Settler>();
    [SerializeField] private GameObject loseScrn;

    public void AddSettler(Settler s)
    {
        settlers.Add(s);
    }
    //public void RemoveSettler(Settler s)
    //{
    //}

    IEnumerator Lose()
    {
        loseScrn.SetActive(false);
        Debug.Log("Started lose game sequence at : " + Time.time);
        yield return new WaitForSeconds(10);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        //Time.timeScale = 0;
        Destroy(gameObject);
    }
}
