using UnityEngine.UI;
using UnityEngine;

public class StartMoney : MonoBehaviour {

	
	void Start () {
        GetComponent<Text>().text = PlayerPrefs.GetInt("Money").ToString();
	}
	
	
}
