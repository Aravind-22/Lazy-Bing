using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput,verticalInput;
    float walkingSpeed = 2f;
    public Animator animator;
    Rigidbody playerRb;
    Vector3 sitRotation;
    public AudioSource playerWalk;
    public AudioClip walk;
    public GameObject pressToSit;
    public GameObject restTimer;
    public GameObject restTimerCount;
    bool isWalking;
    public bool isSit = false;
    public bool isKeyPressed = false;
    public static PlayerMovement instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerRb = GetComponent<Rigidbody>();
        playerWalk = GetComponent<AudioSource>();
        sitRotation = new Vector3(0, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((verticalInput != 0) || (horizontalInput != 0))
        {
            animator.SetFloat("mag", 1);
        }
        if ((verticalInput == 0) && (horizontalInput == 0))
        {
            animator.SetFloat("mag", 0);
        }
        if (isSit)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isKeyPressed = true;
                pressToSit.SetActive(false);
                GameManager.instance.onPause = true;
                transform.position = new Vector3(48,0,70);
                transform.eulerAngles = sitRotation;
                animator.SetFloat("rest", 1);
                walkingSpeed = 0;
            }
        }
        if (isKeyPressed)
        {
            TimeManager.instance.restingChair.GetComponent<BoxCollider>().enabled = false;
            TimeManager.instance.restingTimer -= Time.deltaTime;
            TimeManager.instance.energyText.GetComponent<Text>().text = "Taking rest";
            restTimer.SetActive(true);
            restTimerCount.GetComponent<Text>().text = Mathf.RoundToInt(TimeManager.instance.restingTimer).ToString();
            if (TimeManager.instance.restingTimer <= 0)
            {
                isKeyPressed = false;
                GetUp();
            }
        }
        if (GameManager.instance.onPause==false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.instance.onEscape();
            }
        }     
    }
    private void FixedUpdate()
    {
            Movement();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerWalk.clip = walk;
        Vector3 playerMovement = new Vector3(horizontalInput, 0,verticalInput);
        playerMovement = transform.TransformDirection(playerMovement);
        playerRb.MovePosition(transform.position + playerMovement * walkingSpeed * Time.deltaTime);

        if (playerMovement.magnitude > 0)
            isWalking = true;
        else
            isWalking = false;
        if (isWalking)
        {
            if (!playerWalk.isPlaying)
                playerWalk.Play();
        }
        else
            playerWalk.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        pressToSit.SetActive(true);
        if (other.gameObject.tag == "Chair")
        {
            isSit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pressToSit.SetActive(false);
    }
    void GetUp()
    {
            isSit = false;
            GameManager.instance.onPause = false;
            animator.SetFloat("rest", 0);
            transform.position = new Vector3(47, 0, 70);
            walkingSpeed = 2;
            TimeManager.instance.resetTimer = true;
            restTimer.SetActive(false);
    }
    //public void Save()
    //{
    //    SaveSystem.SaveGame(null, this, null);
    //}
}
