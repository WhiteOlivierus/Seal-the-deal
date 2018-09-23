using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {
    //Checks which zone the player is in and add points with that info

    [SerializeField]
    private int pointCounter = 0;
    public int pointAdder = 1;
    public int zoneBonus = 2;
    public int pointMax = 100;
    public Image currentPoints;

    public Color waterColor;
    public Color landColor;
    public Color bonezoneColor; 
    public Color overTimeColor;
    
    private bool onLand = true;
    private bool boneZone = false;
    private bool inWater = false;

    private NewCharacterBehaviour player;


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
            currentPoints.color = waterColor;
        }
        else
        {
            inWater = false;
        }

        if (inWater != true) //not in water
        {
            if (player.state == 0) //on land
            {
                boneZone = false;
                onLand = true;
                currentPoints.color = landColor;
            }


            if (player.state == 2) //in boneZone
            {
                boneZone = true;
                onLand = false;
                currentPoints.color = bonezoneColor;
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
