// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DinoScript : MonoBehaviour
// {
//     Animator anim;
//     // Start is called before the first frame update
//     void Start()
//     {
//         anim = GetComponent<Animator>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // move dino forward with right key.
//         if (Input.GetKey(KeyCode.RightArrow))
//         {
//             transform.Translate(0.1f,0,0);
//             anim.SetBool("isJump", true);
//         }
//         else
//         {
//             anim.SetBool("isJump", false);
//             anim.SetBool("isIdle", true);
//         }
//     }

//     // if dino collides with game object with name saw, then play die animation.
//     void OnCollisionEnter2D(Collision2D col)
//     {
//         if (col.gameObject.name == "Saw")
//         {
//             anim.SetBool("isDie", true);
//         }
//     }

// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DinoScript : MonoBehaviour
{
    Animator anim;
    public CharacterController2D controller;

    public float runSpeed = 40f;
    private float startTouchPosition, endTouchPosition;

    float lefthorizontalMove = -40f;
    float righthorizontalMove = 40f;
    float speed = 0.05f;
    bool jump = false;
    bool crouch = false;

    static bool reloadflag =false;
    public Text ScoreText;
    public Text HealthText;
    public static float myscore;
    public static float score = 0;
    public static float health = 50;
    public GameObject cloud;
    public GameObject cloud1;
    public GameObject cloud2;

    // Start is called before the first frame update
    void Start()
    {
        if (reloadflag)
        {
            //score = 0;
            score = PlayerPrefs.GetFloat("Player Score");
            ScoreText.text = "Score: " + score.ToString();
        }
        else
        {
            myscore = PlayerPrefs.GetFloat("Player Score");
            ScoreText.text = "Score: " + myscore.ToString();
        }

        anim = GetComponent<Animator>();
        HealthText.text = "Health: " + health.ToString();


        anim.SetBool("isJump", false);
        anim.SetBool("isDie", false);

    }

    // Update is called once per frame
    void Update()
    {
        //for key input
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector2.up * 20 * Time.fixedDeltaTime);
        }

        //for swipe Jump

        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position.y;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position.y;
                if (endTouchPosition > startTouchPosition)
                {
                    transform.Translate(Vector2.up * 250 * Time.fixedDeltaTime);
                }
            }
        }
    }



    private void FixedUpdate()
    {
        //for key input
        if (Input.GetKey(KeyCode.RightArrow))
        {
            controller.Move(righthorizontalMove * Time.fixedDeltaTime, crouch, jump);
            anim.SetBool("isJump", true);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            controller.Move(lefthorizontalMove * Time.fixedDeltaTime, crouch, jump);
            anim.SetBool("isJump", true);
        }

        else
        {
            anim.SetBool("isJump", false);
        }


         

            if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 8)
            {
                controller.Move(lefthorizontalMove * Time.fixedDeltaTime, crouch, jump);
                anim.SetBool("isJump", true);
            }

            if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 8)
            {

                controller.Move(righthorizontalMove * Time.fixedDeltaTime, crouch, jump);
                anim.SetBool("isJump", true);
            }

        }
        else
        {
            anim.SetBool("isJump", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("cloud"))
        {
            transform.gameObject.transform.parent = cloud.transform;
        }
        if (!collision.gameObject.name.StartsWith("cloud"))
        {
            transform.gameObject.transform.parent = null;
        }

        if (collision.gameObject.name.StartsWith("cloud1"))
        {
            transform.gameObject.transform.parent = cloud1.transform;
        }

        if (collision.gameObject.name.StartsWith("cloud2"))
        {
            transform.gameObject.transform.parent = cloud2.transform;
        }


        if (collision.gameObject.name.StartsWith("Apple"))
        {
            health += 10;
            ScoreText.text = "Score: " + score.ToString();
            Destroy(collision.gameObject);
            PlayerPrefs.SetFloat("Player Score", score);
            HealthText.text = "Health: " + health.ToString();

        }

        if (collision.gameObject.name.StartsWith("Coin"))
        {
            score += 10;
            ScoreText.text = "Score: " + score.ToString();
            Destroy(collision.gameObject);
            PlayerPrefs.SetFloat("Player Score", score);
            HealthText.text = "Health: " + health.ToString();

        }


        if (collision.gameObject.name.StartsWith("Saw"))
        {
            health -= 10;
            HealthText.text = "Health: " + health.ToString();
            if (health <= 0)
            {
                anim.SetBool("isDie", true);
            }
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene("SampleScene");
        score = 0;
        PlayerPrefs.SetFloat("Player Score", score);
        reloadflag = true;
    }
}
