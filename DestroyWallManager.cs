using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWallManager : MonoBehaviour
{
    public GameManager gameManager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameManager.Instance.stopPoint == true)
        {
            return;
        }

        if (collision.gameObject.tag == "AirObstacleOne" || collision.gameObject.tag == "GroundObstacleOne" || collision.gameObject.tag == "GroundObstacleTwo")
        {
            // Point
            bool isGroundObstacleOne = collision.gameObject.name.Contains("GroundObstacleOne(Clone)");
            bool isGroundObstacleTwo = collision.gameObject.name.Contains("GroundObstacleTwo(Clone)");
            bool isAirObstacleOne = collision.gameObject.name.Contains("AirObstacleOne(Clone)");

            if(isGroundObstacleOne)
                gameManager.stagePoint += 50;
            else if(isGroundObstacleTwo)
                gameManager.stagePoint += 100;
            else if(isAirObstacleOne)
                gameManager.stagePoint += 150;
        }
    }    
}
