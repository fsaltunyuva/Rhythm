using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                GameManager.instance.NoteHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeInHierarchy)
        {
            if (other.CompareTag("Activator"))
            {
                canBePressed = false;
            }
            GameManager.instance.NoteMissed();
        }

    }
}
