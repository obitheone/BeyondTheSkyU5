using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public static MenuManager Instance;

	public Transform pMainMenu;
	public Transform pOptions;
	public Transform pHelp;
	public Transform pCredits;
	public GameObject resolutionsComboBox;
	public float menuChangeTime;

	private bool MainMenu, Options, Credits, Help;
	private Resolution currentResolution;
	private Vector3 temp;

	void Awake(){
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		Options = Credits = Help = false;
		MainMenu = true;
		currentResolution = Screen.currentResolution;
		fillResolutions ();
	}

	// Update is called once per frame
	void Update () {
		if (MainMenu) {
			temp = pMainMenu.transform.position;
			temp.z = Camera.main.transform.position.z;
		}else if (Options) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				ActivateMenu("MainMenu");
			}
			temp = pOptions.transform.position;
			temp.z = Camera.main.transform.position.z;
		}else if (Help){
			if (Input.GetKeyDown (KeyCode.Escape)) {
				ActivateMenu("MainMenu");
			}
			temp = pHelp.transform.position;
			temp.z = Camera.main.transform.position.z;
		}else if (Credits){
			if (Input.GetKeyDown (KeyCode.Escape)) {
				ActivateMenu("MainMenu");
			}
			temp = pCredits.transform.position;
			temp.z = Camera.main.transform.position.z;
		}

		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, temp, menuChangeTime * Time.deltaTime);	
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

	public void ActivateMenu(string name){
		switch (name) {
		case "MainMenu":
			MainMenu = true;
			Options = Credits = Help = false;
			break;
		case "Credits":
			Credits = true;
			Options = MainMenu = Help = false;
			break;
		case "Help":
			Help = true;
			Options = Credits = MainMenu = false;
			break;
		case "Options":
			Options = true;
			MainMenu = Credits = Help = false;
			break;
		}
	}
		

	public void fillResolutions(){
		Resolution[] resolutions = Screen.resolutions;
		foreach (Resolution res in resolutions) {
			//Aqui viene la magia
			CreateResolution(res);
			//print(res.width + "x" + res.height);
			Debug.Log(res.width + " x " + res.height);
			//Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
		}

	}

	private void CreateResolution(Resolution res){
		var buttonObject = new GameObject (res.width + " x " + res.height);
		var image = buttonObject.AddComponent<Image> ();
		image.transform.SetParent (resolutionsComboBox.transform);
		//image.rectTransform.sizeDelta = new Vector2(180, 50);
		image.rectTransform.anchoredPosition = new Vector3 (0.5f, 0.5f, 1);
		image.rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		image.rectTransform.position = Vector3.zero;
		//image.color = new Color(1f, .3f, .3f, .5f);
		
		var button = buttonObject.AddComponent<Button> ();
		button.targetGraphic = image;
		button.onClick.AddListener (() => Debug.Log (Time.time));
		
		var textObject = new GameObject ("Text");
		textObject.transform.parent = buttonObject.transform;
		var text = textObject.AddComponent<Text> ();
		//text.rectTransform.sizeDelta = Vector2.zero;
		//text.rectTransform.anchorMin = Vector2.zero;
		//text.rectTransform.anchorMax = Vector2.one;

		text.rectTransform.anchoredPosition = new Vector2 (.5f, .5f);
		text.text = res.width + " x " + res.height;
		text.font = Resources.FindObjectsOfTypeAll<Font> () [0];
		text.fontSize = 18;
		text.color = Color.black;
		text.alignment = TextAnchor.MiddleLeft;
		text.rectTransform.position = new Vector3(0f,0f,0f);
	}

}
