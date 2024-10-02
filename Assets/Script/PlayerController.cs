using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어의 움직임 속도를 설정하는 변수
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;     //이동속도
    public float jumpForce = 5.0f;     //점프 힘
    public float rotationSpeed = 10.0f;   //회전 스피디

    [Header("Camera Settings")]
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float mouseSenesitivity = 2.0f;

    public float radius = 5.0f;
    public float minRadius = 1.0f;
    public float maxRadius = 10.0f;

    public float yMinLimit = -90;
    public float yMaxLimit = 90;    

    private float theta = 0.0f;
    private float phi = 0.0f;
    private float targetVerticalRotation = 0;
    private float verticalRotationspeed = 240f;

    public bool isFristPerson = true;
    private bool isGrounded;          //플레이이가 땅에 있는지 여부
    private Rigidbody rb;            //플레이어의 RigidBody
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();   //리지드 바디 컴포넌트를 가져온다.

        Cursor.lockState = CursorLockMode.Locked;  //마우스 커서를 잠그고 숨김
        SetupCameras();
        SetActiveCamera();
    }
    //카메라 및 케릭터 회전 처리하는 함수
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

        //수평회전(theta 값)
        theta += mouseX;
        theta = Mathf.Repeat(theta, 360.0f);

        //수직 회전 처리 
        targetVerticalRotation -= mouseY;
        targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, yMinLimit, yMaxLimit); //수직 회전 제한
        phi = Mathf.MoveTowards(phi, targetVerticalRotation, verticalRotationspeed * Time.deltaTime);

        

        if(isFristPerson)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);

            transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);
        }
        else
        {
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
            thirdPersonCamera.transform.LookAt(transform);

            radius = Mathf.Clamp(radius - Input.GetAxis("Mouse ScrollWheel") * 5, minRadius, maxRadius);
        }

       

    }
    void Update()
    {
        HandleJump();
        
        HandleRotation();
        HandleCameraToggle();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFristPerson);
        thirdPersonCamera.gameObject.SetActive(!isFristPerson);
    }

    //카메라 초기 위치 및 회전을 설정하는 함수

    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0.0f, 0.6f, 0.0f);  //1인칭 카메라 위치
        firstPersonCamera.transform.localRotation = Quaternion.identity;     //1인칭 카메라 회전 초기화

        
    }

    //플레이어 점프를 처리하는 함수
    void HandleJump()
    {
        //점프 버튼을 누르고 땅에 있을때
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);    //위쪽으로 힘을 가해 점프
            isGrounded = false;
        }
        else
        {
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
            thirdPersonCamera.transform.LookAt(transform);

            radius = Mathf.Clamp(radius - Input.GetAxis("Mouse ScrollWheel") * 5, minRadius, maxRadius);
        }
    }

    void HandleCameraToggle()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            isFristPerson = !isFristPerson;
            SetActiveCamera();
        }
    }
    //플레이어의 이동을 처리하는 함수
     void HandleMovement()
    {
        float moveHorizonal = Input.GetAxis("Horizontal");    //좌우 입력(-1 ~ 1)
        float moveVertical = Input.GetAxis("Vertical");       //앞뒤 입력(1 ~ -1)

        Vector3 movement;

        if(!isFristPerson)
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();

            Vector3 cameraRight = thirdPersonCamera.transform.right;
            cameraRight.y = 0.0f;
            cameraRight.Normalize();

            movement = cameraRight * moveHorizonal + cameraForward * moveVertical;
           
        }
        else
        {
            movement = transform.right * moveHorizonal + transform.forward * moveVertical;
            
        }

        if(movement.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }


    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;              //충돌 중이면 플레이어는 땅에 있다
    }
}
