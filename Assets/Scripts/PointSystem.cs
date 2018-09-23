using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {
    //Beweeg naar het object toe voor meer punten ga terug voor weer minder punten 


    [SerializeField]
    private int pointCounter = 0;
    public int pointAdder = 1;
    public int zoneBonus = 2;
    public int pointMax = 100;
    public Image currentPoints;
    public Color overTimeColor;

    private NewCharacterBehaviour player;
    private bool onLand = true;
    private bool boneZone = false;
    private bool inWater = false;


    void Start () {
        StartCoroutine ("PointTimer");
        player = GetComponent<NewCharacterBehaviour> ();
    }


    void FixedUpdate () {
        if (player.state == 1) // in water
        {
            inWater = true;
            onLand = false;
            boneZone = false;
        } else {
            inWater = false;
        }

        if (inWater != true) //not in water
        {
            if (player.state == 0) //on land
            {
                boneZone = false;
                onLand = true;
            }


            if (player.state == 2) //in red zone
            {
                boneZone = true;
                onLand = false;
            }
        }

        if (pointCounter >= pointMax) {
            pointCounter = pointMax;
            currentPoints.color = overTimeColor;
            StopCoroutine ("PointTimer");
        }
    }


    IEnumerator PointTimer () {
        while (true) {
            yield return new WaitForSeconds (1);

            if (onLand == true) {
                pointCounter += pointAdder;
                UpdatePointTotal ();
            }

            // add red zone bonus 
            if (boneZone == true) {
                pointCounter += pointAdder + zoneBonus;
                UpdatePointTotal ();
            }
        }
    }

    private void UpdatePointTotal () {
        currentPoints.fillAmount = (float) pointCounter / (float) pointMax;
    }
}
