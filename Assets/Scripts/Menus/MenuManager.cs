using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public static MenuManager Instance;

	public GameObject pMainMenu;
	public GameObject pOptions;
	public GameObject pHelp;
	public GameObject pCredits;
	public GameObject resolutionsComboBox;

	private Resolution currentResolution;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		pMainMenu.SetActive (true);
		pOptions.SetActive (false);
		pHelp.SetActive (false);
		pCredits.SetActive (false);	
		currentResolution = Screen.currentResolution;
		fillResolutions ();
	}

	// Update is called once per frame
	void Update () {
		
		if (pOptions.activeInHierarchy) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				pOptions.SetActive (false);
				pMainMenu.SetActive (true);
			}
		}else if (pHelp.activeInHierarchy){
			if(Input.GetKeyDown(KeyCode.Escape)){
				pHelp.SetActive (false);
				pMainMenu.SetActive (true);
			}
		}else if (pCredits.activeInHierarchy){
			if(Input.GetKeyDown(KeyCode.Escape)){
				pCredits.SetActive (false);
				pMainMenu.SetActive(true);
			}
		}
		
	}
	
	public void ExitGame(){
		Application.Quit ();
	}
	
	public void LoadLevel(string name){
		Application.LoadLevel (name);
	}

	public void LoadLevel(int num){
		Application.LoadLevel (num);
	}

	public void fillResolutions(){
		Resolution[] resolutions = Screen.resolutions;
		foreach (Resolution res in resolutions) {
			//Aqui viene la magia
			var buttonObject = new GameObject(res.width + " x " + res.height);
			var image = buttonObject.AddComponent<Image>();
			image.transform.SetParent(resolutionsComboBox.transform);
			//image.rectTransform.sizeDelta = new Vector2(180, 50);
			image.rectTransform.anchoredPosition = new Vector3 (0.5f, 0.5f, 1);
			image.rectTransform.pivot = new Vector2 (0.5f, 0.5f);
			//image.color = new Color(1f, .3f, .3f, .5f);
			
			var button = buttonObject.AddComponent<Button>();
			button.targetGraphic = image;
			button.onClick.AddListener(() => Debug.Log(Time.time));
			
			var textObject = new GameObject("Text");
			textObject.transform.parent = buttonObject.transform;
			var text = textObject.AddComponent<Text>();
			//text.rectTransform.sizeDelta = Vector2.zero;
			//text.rectTransform.anchorMin = Vector2.zero;
			//text.rectTransform.anchorMax = Vector2.one;
			text.rectTransform.anchoredPosition = new Vector2(.5f, .5f);
			text.text = res.width + " x " + res.height;
			text.font = Resources.FindObjectsOfTypeAll<Font>()[0];
			text.fontSize = 18;
			text.color = Color.black;
			text.alignment = TextAnchor.MiddleLeft;
			//Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);

		}

	}
}
