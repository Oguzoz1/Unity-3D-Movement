using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Attributes")]
    [SerializeField] float playerSpeed;
    [SerializeField] float rotationSpeed;

    float horizontal;
    float vertical;
    float turnVelocity;

    Vector3 movingDir;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        movement();
        rotation();
    }
    void movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movingDir = new Vector3(horizontal, 0f, vertical).normalized;
        if (movingDir.magnitude >= 0.1f)
        {

            controller.Move(movingDir * playerSpeed * Time.deltaTime);
        }
    }
    void rotation()
    {
        if(movingDir != Vector3.zero)
        {
            float tAngle = Mathf.Atan2(movingDir.x, movingDir.z) * Mathf.Rad2Deg; //Rad2Deg is conversion to degrees.
            float angleSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, tAngle, ref turnVelocity, rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angleSmooth, 0f);
        }
    }
