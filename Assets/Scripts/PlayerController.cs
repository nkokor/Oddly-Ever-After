using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerFall()
    {
        animator.SetTrigger("IsFalling"); 
        Debug.Log("Player is falling!");
    }
}

