using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLabScene : MonoBehaviour
{
    [SerializeField] GameObject InteractSprite;
    [SerializeField] GameObject SceneLoader;

    SceneLoader loadScene;

    bool canInteract;

    private void Awake()
    {
        loadScene = SceneLoader.GetComponent<SceneLoader>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                loadScene.LoadNextLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            InteractSprite.SetActive(true);
            canInteract = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            InteractSprite.SetActive(false);
            canInteract = false;
        }
    }
}
