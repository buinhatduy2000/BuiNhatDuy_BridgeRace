using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private IState<Character> currentState;
    [SerializeField] private Animator anim;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Transform brickHolder;

    private float brickHolder_y;

    private string currentAnimation;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Character> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void ChangeAnimation(string animationName)
    {
        if (currentAnimation != animationName)
        {
            anim.SetTrigger(animationName);
            currentAnimation = animationName;
            anim.SetTrigger(currentAnimation);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            Debug.Log("Take brick");

            other.gameObject.transform.SetParent(brickHolder);

            Vector3 pos = Vector3.zero;
            Quaternion rote = brickHolder.transform.rotation;
            pos.y += brickHolder_y;

            other.gameObject.GetComponent<Transform>().transform.localPosition = pos;
            other.gameObject.GetComponent<Transform>().transform.rotation = rote;

            brickHolder_y += 0.2f;
        }
    }



}
