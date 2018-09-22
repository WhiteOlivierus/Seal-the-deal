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
    public int pointMax = 100; 

    public float playerDistance;
    public float bonezoneBorder;
    public float landBorder; 
    public float waterDistance;

    public Transform BoneZone;
    //public Transform LandZone;
    public Transform playerPos;
    public Transform waterZone;

    public Image currentPoints;

    public Color overTimeColor; 

    public Text pointText;


    void Start()
    {
        StartCoroutine("NormalePointTime");
    }


    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(BoneZone.position, transform.position);
        waterDistance = Vector3.Distance(waterZone.position, transform.position); 

        //playerDistance = Vector3.Distance(playerPos.position, transform.position);
        //bonezoneBorder = Vector3.Distance(playerPos.position, transform.position);
        //landBorder = Vector3.Distance(playerPos.position, transform.position);
        pointText.text = ("" + pointCounter);

        //checks zone 

        if ( waterDistance <= 5) // in water
        {
            inWater = true;
            onLand = false;
            boneZone = false;
        }

        else
        {
            inWater = false; 
        }

        if (inWater != true) //not in water
        {
            if (playerDistance > bonezoneBorder) //on land
            {
                boneZone = false;
                onLand = true;
            }


            if (playerDistance < bonezoneBorder) //in red zone
            {
                boneZone = true;
                onLand = false;
            }
        }   

        if (pointCounter >= pointMax)
        {
            pointCounter = pointMax;
            currentPoints.color = overTimeColor; 
            StopCoroutine("NormalePointTime");
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
                UpdatePointTotal();
            }

            // add red zone bonus 
            if (boneZone == true)
            {
                pointCounter += pointAdder + zoneBonus;
                UpdatePointTotal();
            }
        }
    }

    private void UpdatePointTotal()
    {
        currentPoints.fillAmount  = (float)pointCounter / (float)pointMax; 
    }
}


