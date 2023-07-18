using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] float maxY;
    [SerializeField] float minY;

    [SerializeField] bool scale;

    [SerializeField] float followSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 clampPosition = new Vector3(Mathf.Clamp(player.transform.position.x,minX,maxX), Mathf.Clamp(player.transform.position.y,minY,maxY), -10);

        if (scale)
        {
            gameObject.GetComponent<Camera>().orthographicSize = 7;
        }
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);
        transform.position = Vector3.Slerp(transform.position, clampPosition, followSpeed * Time.deltaTime);
    }    

}
