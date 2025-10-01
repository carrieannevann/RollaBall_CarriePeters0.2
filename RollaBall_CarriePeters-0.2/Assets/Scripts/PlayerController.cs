using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public float speed = 10f;
    public TextMeshProUGUI countText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

       void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
   }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0f, movementY);
        rb.AddForce(movement * speed);
    }

       void OnTriggerEnter(Collider other) 
   {
           if (other.gameObject.CompareTag("PickUp")) 
       {
       {
                  if (other.gameObject.CompareTag("PickUp")) 
       {
           other.gameObject.SetActive(false);
           count = count + 1;

           SetCountText();
       }

       }
       }
   other.gameObject.SetActive(false);
   }
}
