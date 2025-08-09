using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform[] lanes; // تعيين 3 نقاط مسارات في الانسبكتور
    int currentLane = 1; // يبدأ في المسار الأوسط
    public float laneSwitchSpeed = 10f;
    public float jumpForce = 5f;
    bool isJumping = false;

    void Start()
    {
        transform.position = new Vector3(lanes[currentLane].position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        HandleInput();

        Vector3 desired = new Vector3(lanes[currentLane].position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desired, Time.deltaTime * laneSwitchSpeed);

        transform.Translate(Vector3.forward * GameManager.Instance.gameSpeed * Time.deltaTime);
    }

    void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Ended)
            {
                if (t.position.x < Screen.width / 2) MoveLeft();
                else MoveRight();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        }
    }

    public void MoveLeft()
    {
        currentLane = Mathf.Clamp(currentLane - 1, 0, lanes.Length - 1);
    }

    public void MoveRight()
    {
        currentLane = Mathf.Clamp(currentLane + 1, 0, lanes.Length - 1);
    }

    public void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TimeCapsule"))
        {
            GameManager.Instance.AddScore(50);
            TimeManager.Instance.SlowTime(2f, 0.6f);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
