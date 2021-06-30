using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("Hitted");
            other.gameObject.GetComponent<EnemyHealth>().Hitted(damage);
        }
    }
}
