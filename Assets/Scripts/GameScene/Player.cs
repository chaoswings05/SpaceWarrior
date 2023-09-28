using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private GameObject player = null;
    [SerializeField, Header("移動速度")] private float moveSpeed = 1f;
    [SerializeField, Header("ジャンプ力")] private float jumpForce = 1f;
    [SerializeField, Header("地面チェック用オブジェクト")] private Transform groundChecker = null;
    [SerializeField, Header("地面レイヤー")] private LayerMask groundLayer;
    private bool IsGrounded = false;
    [SerializeField, Header("壁チェック用オブジェクト")] private Transform wallChecker = null;
    private bool canGrab = false;
    private bool IsGrabbing = false;
    private float gravityStore = 0f;
    [SerializeField] private float wallJumpTime = 0.2f;
    private float wallJumpCount = 0f;
    [SerializeField] private GameObject arrow = null;
    [SerializeField] private Transform arrowGenPos = null;
    private bool IsShotArrow = false;
    private bool GameEnd = false;
    public bool GetThunder = false;
    public bool GetFire = false;
    public bool GetWater = false;
    public bool GetWind = false;
    public int GetNum = 0;
    [SerializeField] private UIControl uIControl = null;
    [SerializeField] private ResultFade resultFade = null;

    // Start is called before the first frame update
    void Start()
    {
        gravityStore = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnd)
        {
            return;
        }

        PlayerMovement();

        if (!IsGrounded && !IsShotArrow && !IsGrabbing && Input.GetMouseButtonDown(1))
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            GameObject arrowShot = Instantiate(arrow, arrowGenPos.position, Quaternion.identity);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;
            arrowShot.GetComponent<ArrowShot>().SetVector(shotForward, this.gameObject);
            IsShotArrow = true;
        }

        if (!IsGrounded && rb.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
            animator.SetBool("IsJumping", false);
        }
    }

    private void PlayerMovement()
    {
        if (wallJumpCount <= 0)
        {
            if (!IsShotArrow)
            {
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    animator.SetBool("IsRunning", true);
                }
                else
                {
                    animator.SetBool("IsRunning", false);
                }
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
            }

            IsGrounded = Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayer);
            if (IsGrounded)
            {
                animator.SetBool("IsFalling", false);
            }

            //地面に着いている時にマウス左クリックするとジャンプする
            if (Input.GetMouseButtonDown(0) && IsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetBool("IsJumping", true);
            }

            //向きを調整する
            if (rb.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1, 1f);
            }

            canGrab = Physics2D.OverlapCircle(wallChecker.position, 0.2f, groundLayer);

            IsGrabbing = false;
            if (canGrab && !IsGrounded)
            {
                IsGrabbing = true;
                animator.SetBool("IsGrabbing", true);
            }

            if (IsGrabbing)
            {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;

                if (Input.GetMouseButtonDown(0))
                {
                    wallJumpCount = wallJumpTime;

                    rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, jumpForce);
                    rb.gravityScale = gravityStore;
                    IsGrabbing = false;
                }
            }
            else
            {
                animator.SetBool("IsGrabbing", false);
                if (!IsShotArrow)
                {
                    rb.gravityScale = gravityStore;
                }
            }
        }
        else
        {
            wallJumpCount -= Time.deltaTime;
        }
    }

    public void ResetRb()
    {
        if (GameEnd)
        {
            return;
        }

        rb.gravityScale = gravityStore;
        IsShotArrow = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Damage") || other.gameObject.CompareTag("Enemy"))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            Destroy(player);
            GameEnd = true;
            uIControl.ShowDeadEndUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            GameEnd = true;
            if (GetNum == 4)
            {
                Debug.Log("GoodEnd");
                resultFade.StartFade("Good");
            }
            else if (GetNum > 0 && GetNum < 4)
            {
                Debug.Log("NormalEnd");
                resultFade.StartFade("Normal");
            }
            else if (GetNum == 0)
            {
                Debug.Log("BadEnd");
                resultFade.StartFade("Bad");
            }
        }
    }
}
