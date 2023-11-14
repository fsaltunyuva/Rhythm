using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite defaultImage;
    public Sprite pressedImage;
    public KeyCode keyToPress;
    
    public AudioSource music;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            spriteRenderer.sprite = pressedImage;
        }
        
        if(Input.GetKeyUp(keyToPress))
        {
            spriteRenderer.sprite = defaultImage;
        }
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            Time.timeScale = 7f;
            music.pitch = 7f;
        }
        
        if(Input.GetKeyUp(KeyCode.L))
        {
            Time.timeScale = 1f;
            music.pitch = 1f;
        }
    }
}
