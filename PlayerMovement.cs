using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mouseSens;

    float power = 0;
    float zRot = 0;
    float yRot = 0;

    private Animator anim;
    private Rigidbody rb;
    public LayerMask whatIsPlayer;
    Vector3 pos;
    bool evadeRight = true;
    bool evadeLeft = true;
    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.localRotation = Quaternion.Euler(0f, yRot, zRot);
        yRot += mouseX;
        zRot -= mouseY;
        zRot = Mathf.Clamp(zRot, -90f, 90f);

        if (Input.GetKey(KeyCode.Space) && power < 10f && evadeLeft && evadeRight)
        {
            anim.SetBool("Flapping", true);
            anim.SetBool("Flapped", false);
            power += 0.05f;
            if(rb.velocity.x < 0)
            {
                rb.AddForce(rb.velocity.normalized * -0.1f);
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = transform.right * -power;
            power = 0;
            anim.SetBool("Flapped", true);
            anim.SetBool("Flapping", false);
        }

        EvadeRight();
        EvadeLeft();
    }

    void EvadeRight()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rb.AddForce(transform.forward * 75f, ForceMode.Impulse);
            pos = (transform.forward * 50f) + transform.position;
            evadeRight = true;
        }

        Collider[] rightPoints = Physics.OverlapSphere(pos, 10f, whatIsPlayer);

        for (int i = 0; i < rightPoints.Length; i++)
        {
            if (rightPoints[i] && evadeRight)
            {
                rb.velocity = new Vector3(0, 0, 0);
                evadeRight = false;
            }
        }
    }

    void EvadeLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(-transform.forward * 75f, ForceMode.Impulse);
            pos = (-transform.forward * 50f) + transform.position;
            evadeLeft = true;
        }

        Collider[] leftPoints = Physics.OverlapSphere(pos, 10f, whatIsPlayer);

        for (int i = 0; i < leftPoints.Length; i++)
        {
            if (leftPoints[i] && evadeLeft)
            {
                rb.velocity = new Vector3(0, 0, 0);
                evadeLeft = false;
            }
        }
    }
}