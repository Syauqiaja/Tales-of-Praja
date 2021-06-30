using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;

    private CharacterController controller = null;
    private float _ANGLEREF, _SANGLEREF;

    public GameObject weaponCollider;
    public Animator animator;
    public float beginSpeed = 1f;
    public float currSpeed;
    public float runSpeed = 3f;

    private Transform m_camera = null;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null) Instance = this;

        controller = GetComponent<CharacterController>();
        currSpeed = beginSpeed;
        m_camera = Camera.main.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 m_direction = new Vector3(x, 0f, y).normalized;
        if(m_direction.magnitude >= 0.1f){
            currSpeed = Mathf.SmoothDamp(currSpeed, runSpeed,ref _SANGLEREF, 0.5f);
            float targetAngle = Mathf.Atan2(m_direction.x, m_direction.z) * Mathf.Rad2Deg + m_camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _ANGLEREF, 0.1f);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if(!animator.GetCurrentAnimatorStateInfo(0).IsTag("LightAttack")){
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * currSpeed * Time.deltaTime);
            }
        }else{
            currSpeed = Mathf.SmoothDamp(currSpeed, beginSpeed,ref _SANGLEREF, 0.5f);
        }

        if(Input.GetButtonDown("Fire1")){
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsTag("LightAttack") && stateInfo.normalizedTime >= 0.2f && !animator.GetBool("IsAttacking")){
                animator.SetBool("IsAttacking", true);
            }else if(!stateInfo.IsTag("LightAttack")){
                animator.SetBool("IsAttacking", true);
            }
        }
    }

    public void AttackDash(float dashValue){
        StartCoroutine(IEDash(dashValue));
    }

    IEnumerator IEDash(float dashValue){
        
        while (dashValue>0f)
        {
            controller.Move(transform.forward.normalized *0.01f);
            dashValue -= 2*Time.deltaTime;
            yield return null;
        }
        
    }
}