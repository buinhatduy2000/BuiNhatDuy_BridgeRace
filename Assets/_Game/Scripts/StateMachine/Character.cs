using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private IState<Character> currentState;
    [SerializeField] private Animator anim;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] public int numColor;
    [SerializeField] protected float moveSpeed;
    [SerializeField] public Transform brickHolder;
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected Transform pointCheckMovement;
    [SerializeField] public bool canMove;
    [SerializeField] protected LayerMask buildBrickLayer;
    [SerializeField] protected LayerMask groundLayer;
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

        Debug.DrawRay(pointCheckMovement.position, Vector3.down * Mathf.Infinity, Color.red);
        if (Physics.Raycast(pointCheckMovement.position, down, out hit, Mathf.Infinity, buildBrickLayer))
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
        else if (Physics.Raycast(pointCheckMovement.position, down, out hit, Mathf.Infinity, groundLayer))
        {
            canMove = true;
        }
        else
        {
            canMove = false;
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

    public void RemoveBrickFromHolder()
    {
        brickHolder.GetChild(brickHolder.childCount - 1).gameObject.GetComponent<Brick>().ReSpawnBrick();
        brickHolder_y -= 0.2f;
    }

    public Material GetCurrentMaterial()
    {
        return _renderer.material;
    }

    public int GetNumColor()
    {
        return numColor;
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
            Brick brick = other.GetComponent<Brick>();

            if (brick.GetNumColor() == this.numColor)
            {
                brick.transform.SetParent(brickHolder);
                Vector3 newPosition = brickHolder.position;
                Quaternion rote = brickHolder.rotation;
                brick.tag = "Untagged";

                newPosition.y += brickHolder_y;
                brick.transform.position = newPosition;
                brick.transform.rotation = rote;
                brick.transform.localScale = new Vector3(1f,1f,1f);
                brickHolder_y += 0.2f;
            }
            else
            {
                //Debug.Log("Not true color");
            }
        }
    }
}
