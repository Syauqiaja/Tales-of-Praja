using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float healthPoint = 100f;
    
    [SerializeField] private Animator animator;
    private Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Hitted(float damage){
        healthPoint -= damage;
        _rigidbody.AddRelativeForce(-2f * Vector3.forward, ForceMode.Impulse);
        animator.SetTrigger("Hitted");
    }
    
}
