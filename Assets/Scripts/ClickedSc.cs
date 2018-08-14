using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedSc : MonoBehaviour {

    public bool clickedIs = false;

    public void OnClick()
    {
        clickedIs = true;
    }
    public void OnClickUp()
    {
        clickedIs = false;
    }
    
}
