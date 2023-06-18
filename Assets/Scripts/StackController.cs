using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public List<GameObject> balls;
    public GameObject currentBall, prevBall;
    public float delayTime;
    public float current, prev;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StackMovement();
        /*current = currentFeeder.transform.position.x;
        prev = prevFeeder.transform.position.x;
        current = Mathf.Lerp(current, prev, 2f);*/

        //currentFeeder.transform.position = Vector3.Lerp(currentFeeder.transform.position, new Vector3(prevFeeder.transform.position.x, prevFeeder.transform.position.y, prevFeeder.transform.position.z + 2), 20f* Time.deltaTime);

        
    }

    public void StackMovement()
    {
        for (int i = 1; i < balls.Count; i++)
        {
            //print(i);
            prevBall = balls[i - 1];
            currentBall = balls[i]; //current previous ko follow krre

            Vector3 prevPos = prevBall.transform.position;
            
            Vector3 currentPos = currentBall.transform.position;

            //currentFeeder.transform.position = Vector3.Lerp(currentPos, new Vector3(prevPos.x , prevPos.y, prevPos.z + 1), 15f * Time.deltaTime);

            currentBall.transform.position = Vector3.Lerp(currentBall.transform.position, new Vector3(prevBall.transform.position.x, prevBall.transform.position.y, prevBall.transform.position.z + 1.05f), delayTime * Time.deltaTime);
        }
    }
}
