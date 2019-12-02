using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class YuKaMove : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;

    private int JUMP = Animator.StringToHash("Base Layer.jump");
    private int ATTACK = Animator.StringToHash("Base Layer.attack");
    private int DAMAGED = Animator.StringToHash("Base Layer.damaged");
    private int MOVE = Animator.StringToHash("Base Layer.move");
    private int DIE = Animator.StringToHash("Base Layer.die");
    private int IDLE = Animator.StringToHash("Base Layer.Idle_a");

    // Start is called before the first frame update
    void Start()
    {
        animator =GetComponent<Animator>();
        animatorStateInfo = GetComponent<AnimatorStateInfo>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        animator.SetTrigger("jump");
    }
}
