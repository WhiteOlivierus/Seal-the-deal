using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointSystem : MonoBehaviour
{
    //Beweeg naar het object toe voor meer punten ga terug voor weer minder punten 

    public bool onLand = true;
    public bool boneZone = false;
    public bool inWater = false; 

    public int pointCounter = 0;
    public int pointAdder= 1;
    public int zoneBonus = 2; 

    public float zoneDistance;
    public float zoneBorder;
    public float waterDistance; 

    public Transform BoneZone;

    public Text pointText;


    void Start()
    {
        StartCoroutine("NormalePointTime");
    }


    void FixedUpdate()
    {
        zoneDistance = Vector3.Distance(BoneZone.position, transform.position);
        pointText.text = ("" + pointCounter);

        //checks zone 


        if (inWater != true)
        {
            if (zoneDistance > zoneBorder)
            {
                boneZone = false;
                onLand = true;
            }


            if (zoneDistance < zoneBorder)
            {
                boneZone = true;
                onLand = false;
            }
        }   
    }


    IEnumerator NormalePointTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (onLand == true)
            {
                pointCounter += pointAdder;
            }

            // add red zone bonus 
            if (boneZone == true)
            {
                pointCounter += pointAdder + zoneBonus; 
            }
        }
    }    
}


