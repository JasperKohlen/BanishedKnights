using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    private float projectileSpeed;
    public float minSpeed;
    public float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        projectileSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.right * (projectileSpeed * Time.deltaTime));

        projectileSpeed = Random.Range(minSpeed, maxSpeed);

        if (gameObject.transform.position.x > 350)
        {
            Destroy(gameObject);
        }
    }
}
