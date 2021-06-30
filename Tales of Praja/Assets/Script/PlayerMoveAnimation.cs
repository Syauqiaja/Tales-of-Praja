using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAnimation : MonoBehaviour
{
    Animator animator = null;

    public float speed = 0.5f;
    private float m_velocity = 0f;
    // Start is called before the first frame update


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 m_direction = new Vector3(x, 0f, y);

        if(m_direction.magnitude >= 0.1f && m_velocity <= 1f && !animator.GetCurrentAnimatorStateInfo(0).IsTag("LightAttack")){
            m_velocity += speed * Time.deltaTime;
            animator.SetBool("IsRunning", true);
            animator.SetFloat("Velocity", m_velocity);
        }else if(m_velocity > 0f)
        {
            animator.SetBool("IsRunning", false);
            m_velocity -= speed * Time.deltaTime *2;
            animator.SetFloat("Velocity", m_velocity);
        }

        if(m_velocity < 0f) {
            m_velocity = 0f;
            animator.SetFloat("Velocity", m_velocity);
        }
    }
}
