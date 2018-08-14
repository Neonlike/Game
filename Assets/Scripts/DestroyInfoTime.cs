using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInfoTime : MonoBehaviour {

    [SerializeField]
    private float destroyTimer = 2f;
    void Awake()
    {
        StartCoroutine(DestroyInfo());

    }

    IEnumerator DestroyInfo()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
}
