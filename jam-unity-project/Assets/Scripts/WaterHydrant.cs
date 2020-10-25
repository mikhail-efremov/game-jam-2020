using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHydrant : MonoBehaviour
{
    [SerializeField] private GameObject _waterFx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_waterFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
