using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float CurSpeed
    {
        get
        {
            return (speed * (Input.GetKey(KeyCode.C) ? sitSpeed : 1)) * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : 1);
        }
    }

private CharacterController _cC;

    private float ySpeed;

    public Vector3 velocity = Vector3.zero;

    public Vector3 moveDir = Vector3.zero;

    [SerializeField] private float speed = 5f;

    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private float sitSpeed = 2f;

    [SerializeField] private float jumpPower = 5f;

    [SerializeField] private float rotationSpd = 5f;


    [SerializeField] private int jumpCnt = 2;

    public bool isJump = false;

    public bool isSit = false;
    public bool isRun = false;
    float magnitude;
    void Start()
    {
        _cC = GetComponent<CharacterController>();
    }
    void Update()
    {
        Move();
        //Run();
        //Sit();
        Jump();
    }
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(h, 0, v);
         magnitude = Mathf.Clamp01(moveDir.magnitude);
        moveDir.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;
        velocity = moveDir * magnitude* CurSpeed;
        velocity.y = ySpeed;

        _cC.Move(velocity * Time.deltaTime);

        if (moveDir != Vector3.zero)
        {
             Quaternion toRot = Quaternion.LookRotation(moveDir, Vector3.up);
             transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotationSpd * Time.deltaTime);
        }
    }
    private void Sit()
    {

        if (Input.GetKey(KeyCode.C))
        {
            Debug.Log("A");
            velocity = moveDir * magnitude * sitSpeed;
            _cC.Move(velocity * Time.deltaTime);

            //  _cC.Move(velocity * sitSpeed * Time.deltaTime); //애만 적용ㅇ
            //앉는 애니메이션
        }
    }
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity = moveDir * runSpeed * sitSpeed;
            _cC.Move(velocity * Time.deltaTime);

            //_cC.Move(velocity * runSpeed * Time.deltaTime);
            //달리는 애니메이션
        }
    }
    private void Jump()
    {
        if (!isJump && jumpCnt > 0)
        {
            ySpeed = -0.5f;

            if (Input.GetButton("Jump"))
            {
                ySpeed = jumpPower;
                jumpCnt--;
            }
        }
    }
}
