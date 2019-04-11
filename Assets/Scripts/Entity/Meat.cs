using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(gameObject);
    }

    void Update()
    {
        StartCoroutine("Delete");
    }
}
