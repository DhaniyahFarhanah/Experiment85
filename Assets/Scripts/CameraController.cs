using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Camera controller allows the camera to move in a certain set bounds and follows player.

public class CameraController : MonoBehaviour
{
    // Script done by: Nana (Dhaniyah Farhanah Binte Yusoff)


    GameObject player;

    //the min and max for the camera to be allowed to move within
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] float maxY;
    [SerializeField] float minY;

    [SerializeField] bool scale; //allows more customizability. Value to scale the camera if needed
    [SerializeField] int scaleSize; //allows change of scale size

    [SerializeField] float followSpeed; //the speed that the camera follows character (because it will lay behind using slerp) 

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
        //allows set camera movement
        Vector3 clampPosition = new Vector3(Mathf.Clamp(player.transform.position.x,minX,maxX), Mathf.Clamp(player.transform.position.y,minY,maxY), -10);

        if (scale) //tried it do this because the wave gameplay seems too near with so many enemies but it's fun to suffer >:)
        {
            gameObject.GetComponent<Camera>().orthographicSize = scaleSize;
        }

        //old code
        //Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);

        //changes camera position
        transform.position = Vector3.Slerp(transform.position, clampPosition, followSpeed * Time.deltaTime);
    }    

}
