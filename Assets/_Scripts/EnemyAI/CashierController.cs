using UnityEngine;

public class CashierController : MonoBehaviour
{
    private CheckOut checkout;
    private TaskList taskList;
    public GameObject CheckoutText;

    private void Start()
    {
        // Get reference to TaskList script in the scene
        checkout = FindObjectOfType<CheckOut>();
        taskList = FindObjectOfType<TaskList>();
        CheckoutText.SetActive(false);
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && checkout != null)
        {
            if (taskList.canCheckout) // Only allow checkout if all tasks are completed
            {
                CheckoutText.SetActive(true);

                if (Input.GetKey(KeyCode.E))
                {

                    // Call the checkout method when player enters cashier area
                    checkout.Checkout();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckoutText.SetActive(false);
    }
}
