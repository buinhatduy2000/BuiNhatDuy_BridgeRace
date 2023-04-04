using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private IState<Character> currentState;
    [SerializeField] private Animator anim;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected int numColor;
    [SerializeField] protected float moveSpeed;
    [SerializeField] public Transform brickHolder;
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected Transform pointCheckMovement;
    [SerializeField] public bool canMove;
    [SerializeField] protected LayerMask buildBrickLayer;
    private float brickHolder_y;
    private string currentAnimation;

    public virtual void Start()
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

    private void FixedUpdate()
    {
        CheckCanMove();
    }

    private void CheckCanMove()
    {
        RaycastHit hit;
        Vector3 down = pointCheckMovement.TransformDirection(Vector3.down);
        Debug.DrawRay(pointCheckMovement.position, Vector3.down * 100, Color.red);
        if (Physics.Raycast(pointCheckMovement.position, down, out hit, 100, buildBrickLayer))
        {
            BuildBrick buildBrick = hit.collider.GetComponent<BuildBrick>();

            if (!HasBricksInHolder() && buildBrick.numberColor != GetNumColor())
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }
        else
        {
            canMove = true;
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

    public void SetCharacterColor(Material material, int color)
    {
        _renderer.material = material;
        numColor = color;
    }
    public bool HasBricksInHolder()
    {
        return brickHolder.childCount > 0;
    }

    public bool RemoveBrickFromHolder()
    {
        if (brickHolder.childCount > 0)
        {
            Destroy(brickHolder.GetChild(0).gameObject);
            brickHolder_y -= 0.2f;
            return true;
        }
        return false;
    }

    public Material GetCurrentMaterial()
    {
        return _renderer.material;
    }

    public int GetNumColor()
    {
        return numColor;
    }

    protected void ChangeAnimation(string animationName)
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
            Brick brick = other.GetComponent<Brick>();

            if (brick.GetNumColor() == this.numColor)
            {
                GameObject stackedBrick = Instantiate(brick.gameObject, brickHolder.position, Quaternion.identity);
                stackedBrick.transform.SetParent(brickHolder);
                Vector3 newPosition = brickHolder.position;
                Quaternion rote = brickHolder.rotation;
                stackedBrick.tag = "Untagged";

                newPosition.y += brickHolder_y;
                stackedBrick.transform.position = newPosition;
                stackedBrick.transform.rotation = rote;
                brickHolder_y += 0.2f;

                //Destroy(other.gameObject);
                brick.PickUp();
            }
            else
            {
                Debug.Log("Not true color");
            }
        }
    }
}