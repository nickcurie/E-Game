using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Text funnyText;

    public int score { get; set; }

    private Animator anim;
    private CharacterController2D controller;
    private GameObject linkedDoor;
    private HealthBarHandler healthBar;
    private float horizontalMove = 0f;
    private bool jump = false;
    private float size = 60f;
    private bool reverse;
    private bool hasTeleported;
    private bool textPopRunning;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        score = 0;
        healthBar = GetComponentInChildren<HealthBarHandler>();
        controller = GetComponent<CharacterController2D>();
        //healthBar = gameObject.child
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (horizontalMove > 0 || horizontalMove < 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        //UnityEngine.Debug.Log(horizontalMove);
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            jump = true;
            if (!(anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump")))
            {
                anim.SetTrigger("jump");
            }
        }
        
        funnyText.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
        if (size > 72)
            reverse = true;

        if (size < 60)
            reverse = false;

        if (reverse)
        {
            //funnyText.fontSize -= 1 * Time.deltaTime;
            size -= 1 * Time.deltaTime;
        }
        else
        {
            //funnyText.fontSize += 1 * Time.deltaTime;
            size += 1 * Time.deltaTime;
        }
        
        funnyText.fontSize = Mathf.RoundToInt(size);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        scoreText.text = "Kills: " + score;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //UnityEngine.Debug.Log(other.tag);
        if (other.tag == "Killbox")
        {
            transform.position = respawnPoint.transform.position;
            score -= 10;
        }
        else if (other.tag == "Enemy")
        {
            //UnityEngine.Debug.Log("ouch!");
            healthBar.SetHealthBarValue(healthBar.GetHealthBarValue() - 0.05f);
            if(healthBar.GetHealthBarValue() < 0.05f){
                // transform.position = respawnPoint.transform.position;
                score -= 20;
                healthBar.SetHealthBarValue(1.0f);
            }
        }
        else if (other.tag == "Button")
        {
            other.gameObject.GetComponent<Animator>().enabled = true; 
            linkedDoor = GameObject.Find("Door");
            linkedDoor.GetComponent<Animator>().enabled = true;
        }
        else if (other.tag == "Vault"){
            if (score > 0){
                PlayerData.playerMoney += score;
                score = 0;
                Debug.Log("money saved");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Portal"){
            if (!hasTeleported){
                transform.position = other.gameObject.GetComponent<Teleporter>().GetDestination().position;
                if (!textPopRunning){
                    StartCoroutine("PopUp");
                }
                hasTeleported = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Portal"){
            hasTeleported = false;
        }
    }

    private IEnumerator PopUp()
    {   
        textPopRunning = true;
        scoreText.fontSize = 70;

        for (int framecnt = 0; framecnt < 10; framecnt++)
        {
            //Debug.Log(framecnt);
            yield return new WaitForEndOfFrame();
        }

        scoreText.fontSize = 60;
        textPopRunning = false;
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        UnityEngine.Debug.Log("ouch!");
    //        score -= 10;
    //    }
    //}
}
