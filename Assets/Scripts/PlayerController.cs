using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform cam;

    public float baseSpeed = 1f;
    public float maxSpeed = 6f;
    public float acceleration = 0.5f;
    public float turnSmoothTime = 0.1f;

    private float currentSpeed;
    private float turnSmoothVelocity;

    public Animator animator;

    private bool isJumping = false;

    public int numberOfLives = 2;
    private float currentHealthStatus;
    private Vector3 initialPosition; 
    public bool isInvulnerable = false; 

    public TextMeshProUGUI livesText;  
    public Slider healthSlider;  

    private int damageTakenThisLife = 0;

    public Image blackoutImage;

    void Start()
    {
        currentSpeed = 0f;
        agent.speed = baseSpeed;
        currentHealthStatus = 1f;
        initialPosition = new Vector3(0.03931332f, 0.328f, 1.65f);

        healthSlider.maxValue = 3;
        healthSlider.value = 3;

        blackoutImage.gameObject.SetActive(false); 
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateUI();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        bool isMoving = direction.magnitude >= 0.1f;

        if (isMoving)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            agent.speed = currentSpeed;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            agent.SetDestination(transform.position + moveDirection);
        }
        else
        {
            currentSpeed = 0f;
            agent.speed = 0f;
        }

        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Speed", currentSpeed);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnFallToIdleTransition()
    {
        isJumping = false;
        animator.SetBool("IsJumping", false);
    }

    public void TriggerFall()
    {
        if (isInvulnerable) return;  

        animator.SetTrigger("IsFalling");
        SoundManager.Instance.PlaySound3D("Scream", transform.position);
        SoundManager.Instance.PlaySound3D("Fall", transform.position);

        TakeDamage();
    }

    private void TakeDamage()
    {
        damageTakenThisLife++; 
        if (damageTakenThisLife >= 3) {
            currentHealthStatus = 1f; 
            healthSlider.value = currentHealthStatus * 3; 
        } else {
            currentHealthStatus -= 1f / 3f; 
            healthSlider.value = currentHealthStatus * 3; 
        }

        if (currentHealthStatus <= 0f)
        {
            currentHealthStatus = 1f; 
            healthSlider.value = currentHealthStatus * 3; 
        }

        healthSlider.value = currentHealthStatus * 3; 

        if (damageTakenThisLife >= 3)
        {
            currentHealthStatus = 1f; 
            healthSlider.value = currentHealthStatus * 3; 
            numberOfLives--;
            damageTakenThisLife = 0; 

            if (numberOfLives <= 0)
            {
                currentHealthStatus = 0;
                healthSlider.value = currentHealthStatus * 3; 
                StartCoroutine(HandleGameOver());
            }
            else
            {
                StartCoroutine(BlackoutAndResetPosition()); 
            }
        }
        else
        {
            isInvulnerable = true;
            Invoke(nameof(ResetInvulnerability), 4f);
        }
    }

    private IEnumerator BlackoutAndResetPosition()
    {
        blackoutImage.gameObject.SetActive(true); 

        blackoutImage.color = new Color(0, 0, 0, 255); 

        float duration = 1f;
        float elapsed = 0f;
        currentHealthStatus = 1f; 
        healthSlider.value = currentHealthStatus * 3; 
        
        transform.position = initialPosition;  
        
        while (elapsed < duration)
        {
            blackoutImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        elapsed = 0f;
        while (elapsed < duration)
        {
            blackoutImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        blackoutImage.gameObject.SetActive(false); 

        StartCoroutine(ResetInvulnerabilityAndWait());
    }

    private IEnumerator HandleGameOver()
    {
        animator.SetTrigger("IsFalling"); 
        yield return new WaitForSeconds(2f);  
        SceneManager.LoadScene("FailureScene");
    }

    private IEnumerator ResetInvulnerabilityAndWait()
    {
        yield return new WaitForSeconds(4f); 
        isInvulnerable = false;
    }

    private void ResetInvulnerability()
    {
        isInvulnerable = false;
    }

    private void UpdateUI()
    {
        livesText.text = numberOfLives.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("extra_life"))
        {
            SoundManager.Instance.PlaySound3D("Extra Life", transform.position);
            numberOfLives++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("health_elix"))
        {
            SoundManager.Instance.PlaySound3D("Health Elixir", transform.position);
            if (currentHealthStatus < 1f)
            {
                currentHealthStatus += 0.33f;
                healthSlider.value = currentHealthStatus * 3; 
                if (currentHealthStatus > 1f) currentHealthStatus = 1f;
            }
            Destroy(other.gameObject);
        }
    }
}
