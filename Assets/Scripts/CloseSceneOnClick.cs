using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSceneOnClick : MonoBehaviour {

    public void OnMouseUp()
    {
        print("bas is cool"); 
        Application.Quit(); 
    }
}
