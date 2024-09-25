using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�÷��̾��� ������ �ӵ��� �����ϴ� ����
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;     //�̵��ӵ�
    public float jumpForce = 5.0f;     //���� ��

    [Header("Camera Settings")]
    public Camera fristPersonCamera;
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

    private bool isFristPerson = true;
    private bool isGrounded;          //�÷����̰� ���� �ִ��� ����
    private Rigidbody rb;            //�÷��̾��� RigidBody
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();   //������ �ٵ� ������Ʈ�� �����´�.

        Cursor.lockState = CursorLockMode.Locked;  //���콺 Ŀ���� ��װ� ����
        SetupCameras();
        SetActiveCamera();
    }
    //ī�޶� �� �ɸ��� ȸ�� ó���ϴ� �Լ�
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

        //����ȸ��(theta ��)
        theta += mouseX;
        theta = Mathf.Repeat(theta, 360.0f);

        //���� ȸ�� ó�� 
        targetVerticalRotation -= mouseY;
        targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, yMinLimit, yMaxLimit); //���� ȸ�� ����
        phi = Mathf.MoveTowards(phi, targetVerticalRotation, verticalRotationspeed * Time.deltaTime);

        //�÷��̾� ȸ��(�ɸ��Ͱ� �������θ� ȸ��)
        transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

        fristPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);   //1��Ī ī�޶� ���� ȸ��

    }
    void Update()
    {
        HandleJump();
        HandleMovement();
        HandleRotation();
        HandleCameraToggle();
    }

    void SetActiveCamera()
    {
        fristPersonCamera.gameObject.SetActive(isFristPerson);
        thirdPersonCamera.gameObject.SetActive(!isFristPerson);
    }

    //ī�޶� �ʱ� ��ġ �� ȸ���� �����ϴ� �Լ�

    void SetupCameras()
    {
        fristPersonCamera.transform.localPosition = new Vector3(0.0f, 0.6f, 0.0f);  //1��Ī ī�޶� ��ġ
        fristPersonCamera.transform.localRotation = Quaternion.identity;     //1��Ī ī�޶� ȸ�� �ʱ�ȭ

        
    }
    //�÷��̾� ������ ó���ϴ� �Լ�
    void HandleJump()
    {
        //���� ��ư�� ������ ���� ������
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);    //�������� ���� ���� ����
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
    //�÷��̾��� �̵��� ó���ϴ� �Լ�
     void HandleMovement()
    {
        float moveHorizonal = Input.GetAxis("Horizontal");    //�¿� �Է�(-1 ~ 1)
        float moveVertical = Input.GetAxis("Vertical");       //�յ� �Է�(1 ~ -1)

        

        if(!isFristPerson)
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();

            Vector3 cameraRight = thirdPersonCamera.transform.right;
            cameraRight.y = 0.0f;
            cameraRight.Normalize();

            Vector3 movement = cameraRight * moveHorizonal + cameraForward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 movement = transform.right * moveHorizonal + transform.forward * moveHorizonal;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;              //�浹 ���̸� �÷��̾�� ���� �ִ�
    }
}