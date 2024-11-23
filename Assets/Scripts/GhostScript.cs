using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostScript : MonoBehaviour
{
    private Animator Anim;
    private CharacterController Ctrl;
    private Vector3 MoveDirection = Vector3.zero;

    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
    private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
    private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
    private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
    private static readonly int AttackTag = Animator.StringToHash("Attack");

    [SerializeField] private SkinnedMeshRenderer[] MeshR;
    private float Dissolve_value = 1;
    private bool DissolveFlg = false;
    private const int maxHP = 3;
    private int HP = maxHP;


    [SerializeField] private float Speed = 4;

    void Start()
    {
        Anim = this.GetComponent<Animator>();
        Ctrl = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        STATUS();
        GRAVITY();
        Respawn();
   
        if(!PlayerStatus.ContainsValue( true ))
        {
            MOVE();
            PlayerAttack();
            Damage();
        }
        else if(PlayerStatus.ContainsValue( true ))
        {
            int status_name = 0;
            foreach(var i in PlayerStatus)
            {
                if(i.Value == true)
                {
                    status_name = i.Key;
                    break;
                }
            }
            if(status_name == Dissolve)
            {
                PlayerDissolve();
            }
            else if(status_name == Attack)
            {
                PlayerAttack();
            }
            else if(status_name == Surprised)
            {
                
            }
        }
 
        if(HP <= 0 && !DissolveFlg)
        {
            Anim.CrossFade(DissolveState, 0.1f, 0, 0);
            DissolveFlg = true;
        }

        else if(HP == maxHP && DissolveFlg)
        {
            DissolveFlg = false;
        }
    }


    private const int Dissolve = 1;
    private const int Attack = 2;
    private const int Surprised = 3;
    private Dictionary<int, bool> PlayerStatus = new Dictionary<int, bool>
    {
        {Dissolve, false },
        {Attack, false },
        {Surprised, false },
    };

    private void STATUS ()
    {

        if(DissolveFlg && HP <= 0)
        {
            PlayerStatus[Dissolve] = true;
        }
        else if(!DissolveFlg)
        {
            PlayerStatus[Dissolve] = false;
        }
   
        if(Anim.GetCurrentAnimatorStateInfo(0).tagHash == AttackTag)
        {
            PlayerStatus[Attack] = true;
        }
        else if(Anim.GetCurrentAnimatorStateInfo(0).tagHash != AttackTag)
        {
            PlayerStatus[Attack] = false;
        }
 
        if(Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == SurprisedState)
        {
            PlayerStatus[Surprised] = true;
        }
        else if(Anim.GetCurrentAnimatorStateInfo(0).fullPathHash != SurprisedState)
        {
            PlayerStatus[Surprised] = false;
        }
    }
   
    private void PlayerDissolve ()
    {
        Dissolve_value -= Time.deltaTime;
        for(int i = 0; i < MeshR.Length; i++)
        {
            MeshR[i].material.SetFloat("_Dissolve", Dissolve_value);
        }
        if(Dissolve_value <= 0)
        {
            Ctrl.enabled = false;
        }
    }
 
    private void PlayerAttack ()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Anim.CrossFade(AttackState,0.1f,0,0);
        }
    }

    private void GRAVITY ()
    {
        if(Ctrl.enabled)
        {
            if(CheckGrounded())
            {
                if(MoveDirection.y < -0.1f)
                {
                    MoveDirection.y = -0.1f;
                }
            }
            MoveDirection.y -= 0.1f;
            Ctrl.Move(MoveDirection * Time.deltaTime);
        }
    }

    private bool CheckGrounded()
    {
        if (Ctrl.isGrounded && Ctrl.enabled)
        {
            return true;
        }
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        float range = 0.2f;
        return Physics.Raycast(ray, range);
    }

    private void MOVE()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MOVE_Velocity(transform.forward, transform.rotation.eulerAngles);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotatePlayer(-90);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotatePlayer(90);
        }
    }


    private void RotatePlayer(float angle)
    {
        transform.Rotate(0, angle, 0);
    }

    private void MOVE_Velocity(Vector3 velocity, Vector3 rot)
    {
        MoveDirection = transform.forward * Speed;  
        if (Ctrl.enabled)
        {
            Ctrl.Move(MoveDirection * Time.deltaTime);
        }
        MoveDirection.x = 0;
        MoveDirection.z = 0;
    }


    private void KEY_DOWN ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
    }

    private void KEY_UP ()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if(!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
    }

    private void Damage ()
    {

        if(Input.GetKeyUp(KeyCode.S))
        {
            Anim.CrossFade(SurprisedState, 0.1f, 0, 0);
            HP--;
        }
    }

    private void Respawn ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            HP = maxHP;
            
            Ctrl.enabled = false;
            this.transform.position = Vector3.zero;
            Ctrl.enabled = true;
            
            Dissolve_value = 1;
            for(int i = 0; i < MeshR.Length; i++)
            {
                MeshR[i].material.SetFloat("_Dissolve", Dissolve_value);
            }
 
            Anim.CrossFade(IdleState, 0.1f, 0, 0);
        }
    }
}