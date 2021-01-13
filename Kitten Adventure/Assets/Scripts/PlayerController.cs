using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    //Start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D bColl;
    private CapsuleCollider2D cColl;    
    
    //FSM (Finite State Machine)
    private enum State { idle, running, jumping, falling, dead, inBox, endLevel}

    private State state = State.idle;

    //Inspector (unity) variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField]private float maxTime = 30f;
    [SerializeField] private float yarnRed = 5f;
    [SerializeField] private float yarnBlue = 10f;
    [SerializeField] private float yarnGreen = 2f;
    [SerializeField] private float yarnYellow = 7f;
    [SerializeField] private AudioSource purring;
    [SerializeField] private AudioSource pickup;
    [SerializeField] private AudioSource hurt;
    [SerializeField] private Animator transition;


    //Other variables
    public float timeLeft = 0f;    
    private bool frozen = false;
    private bool canWalk = true;
    private bool pauseTimer = true;
    public float transitionTime = 1f;

    //Functions from other scripts
    public HealthBarScript healthBar;
    public LevelLoaderScript levelLoader;
            
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bColl = GetComponent<Collider2D>();
        cColl = GetComponent<CapsuleCollider2D>();               

        timeLeft = maxTime;
        healthBar.SetMaxHealth(maxTime);
    }

    private void Update()
    {
        Movement();
        AnimationState();
        anim.SetInteger("state", (int)state); //sets animation based on Enumerator state
        TimeHandler();     
        if(state == State.inBox)
        {
            InBoxHandler();
        }
        PlayerSoundHandler();
            
    }

    private void PlayerSoundHandler()
    {
        if (!purring.isPlaying && state == State.inBox ^ state == State.endLevel)
        {
            purring.Play();
                        
        }
        else if (purring.isPlaying && state == State.running ^ state== State.jumping)
        { purring.Stop(); }

        if(!hurt.isPlaying && state == State.dead)
        {
            hurt.Play();
        }
        
    }

    private void TimeHandler()
    {
        if (!pauseTimer && timeLeft >= 0f)
        {
            timeLeft -= Time.deltaTime;

            healthBar.SetHealth(timeLeft);
        }
        if (timeLeft > maxTime)
        {
            timeLeft = maxTime;
        }

        if (timeLeft <= 0f)
        {
            frozen = true;
            state = State.dead;
        }

        if(state == State.inBox && timeLeft < maxTime)
        {
            pauseTimer = true;
            timeLeft += Time.deltaTime * 5;
            healthBar.SetHealth(timeLeft);
        }

        if(state == State.running ^ state == State.jumping)
        {
            pauseTimer = false;
        }

        if(state == State.endLevel)
        {
            pauseTimer = true;
          }
    }

    private void InBoxHandler()
    {
        if(timeLeft < maxTime)
        {
            frozen = true;            
          }
        else
        {
            frozen = false;
            state = State.inBox;
            
        }
     }

    //Collectables & fall off map & box & end level box
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collectables
        if (collision.tag == "Collectable" && collision.IsTouching(bColl))
        {
            pickup.Play();

            Destroy(collision.gameObject);

            if (collision.gameObject.name.Contains("red"))
            {
                timeLeft += yarnRed;
            }
            else if (collision.gameObject.name.Contains("blue"))
            {
                timeLeft += yarnBlue;
            }
            else if (collision.gameObject.name.Contains("green"))
            {
                timeLeft += yarnGreen;
            }
            else
            {
                timeLeft += yarnYellow;
            }


        }

        //Fall of the map
        if (collision.tag == "Fall")
        {
            Jump();
            frozen = true;
            state = State.dead;
        }

        //box
        if(collision.tag == "Box")
        {
            state = State.inBox;
            rb.transform.position = collision.transform.position;
            rb.transform.localScale = collision.transform.localScale;
            rb.velocity = new Vector2(0, 0);
            InBoxHandler();
            collision.gameObject.tag = "Untagged";
        }

        //EndLevelBox
        if (collision.tag == "EndLevelBox")
        {
            state = State.endLevel;
            rb.transform.position = collision.transform.position;
            rb.transform.localScale = collision.transform.localScale;
            rb.velocity = new Vector2(0, 0);
            frozen = true;

            levelLoader.LoadNextLevel();
            
        }
    }

    //Hit spike
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Spikes" && state == State.falling)
        {
            Jump();
            frozen = true;
            state = State.dead;
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (!frozen)
        {
            //anti wall-stick
            if (bColl.IsTouchingLayers(ground) && !cColl.IsTouchingLayers(ground))
            {
                canWalk = false;
            }
            else
            {
                canWalk = true;
            }

            //moving left
            if (hDirection < 0 && canWalk == true)
            //(Input.GetKey(KeyCode.A)) eerste versie
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }


            //moving right
            else if (hDirection > 0 && canWalk == true)
            //(Input.GetKey(KeyCode.D)) eerste versie
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }


            //jumping
            if (Input.GetButtonDown("Jump") && cColl.IsTouchingLayers(ground))
            {
                Jump();
                state = State.jumping;
            }
        }
    }

    private void AnimationState()
    {
        if (!frozen)
        {
            if (state == State.running && !cColl.IsTouchingLayers(ground))
            {
                state = State.falling;
            }
            else if (state == State.idle && !cColl.IsTouchingLayers(ground))
            {
                state = State.falling;
            }
            else if (state == State.jumping)
            {
                if (rb.velocity.y < .1f)
                {
                    state = State.falling;
                }
            }
            else if (state == State.falling)
            {
                if (cColl.IsTouchingLayers(ground))
                {
                    state = State.idle;
                }
            }
            else if (Mathf.Abs(rb.velocity.x) > 2f)
            {
                state = State.running;
            }
            else
            {
                state = State.idle;
            }
        }
    }

    private void Jump()
    { 
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Death()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}