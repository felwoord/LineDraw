using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvResizer : MonoBehaviour {
	private CanvasScaler canvScaler;

	// Use this for initialization
	void Start () {
		canvScaler = GetComponent<CanvasScaler> ();

		if (canvScaler.screenMatchMode == CanvasScaler.ScreenMatchMode.MatchWidthOrHeight) {

			/*if (SceneManager.GetActiveScene ().name == "MenuScene"){
			#if UNITY_STANDALONE
			canvScaler.referenceResolution = new Vector2 (1920, 1080);
			#else
			canvScaler.referenceResolution = new Vector2 (1920, 1080);
			#endif
			}*/
			if (SceneManager.GetActiveScene ().name == "GameScene"){
				#if UNITY_STANDALONE
				canvScaler.referenceResolution = new Vector2 (1920, 1080) * 0.75f;
				#else
				canvScaler.referenceResolution = new Vector2 (1920, 1080) * 0.75f;
				#endif
			}

            float width = Display.main.systemWidth;
            float height = Display.main.systemHeight;
            float ratio = width / height;
            if (ratio <= 0.5f)
            {
                if (gameObject.name == "AchievementsCanvas")
                {
                    canvScaler.matchWidthOrHeight = 0.85f;
                }
                if (gameObject.name == "HelpCanvas")
                {
                    canvScaler.matchWidthOrHeight = 0.87f;
                }
                if (gameObject.name == "HelpCanvas")
                {
                    canvScaler.matchWidthOrHeight = 0.87f;
                }
                if (gameObject.name == "EndRunCanvas")
                {
                    canvScaler.matchWidthOrHeight = 0.9f;
                }
                if (gameObject.name == "ContinueCanvas")
                {
                    canvScaler.matchWidthOrHeight = 0.85f;
                }
            }


        }

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
