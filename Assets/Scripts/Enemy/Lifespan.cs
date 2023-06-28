using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    [SerializeField]
    private bool hasLifespan = false;
    [SerializeField]
    private float lifespan = 20f;
    [SerializeField]
    private bool randomiseLifespan = false;
    [SerializeField]
    private float minLifespan = 5f;
    [SerializeField]
    private float maxLifespan = 20f;
    // Start is called before the first frame update
    void Start()
    {
        if(hasLifespan)
        {
            if (randomiseLifespan)
            {
                lifespan = Random.Range(minLifespan, maxLifespan);
            }
            StartCoroutine(EndObjectLife());
        }
    }

    IEnumerator EndObjectLife()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
