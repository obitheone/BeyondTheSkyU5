using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum UI_LifeStyle {Vertical = 1, Radial360, Radial270, Radial180};

public class UI_LifeController : MonoBehaviour {

	public float currentLife;
	public float downRate;
	public Image lifeText;
	public UI_LifeStyle type;
	public Sprite sprite180;
	public Sprite sprite360;
	public Material UI_LifeMaterial;
	public Color FullLife;
	public Color LowLife;
	public float colorChangeSpeed;

	
	private bool updateLife;
	private float maxFillAmount;
	// Use this for initialization
	void Start () {
		currentLife = 100f;
		updateLife = false;
		ChangeType (UI_LifeStyle.Vertical);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.KeypadMultiply)) {
			if (type == UI_LifeStyle.Radial180)
				type = UI_LifeStyle.Vertical;
			else
				type += 1;
			ChangeType(type);
		}
		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			if (currentLife + 20f <= 100f){
				currentLife += 20;
				updateLife = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			if (currentLife - 20f >= 0f){
				currentLife -= 20;
				updateLife = true;
			}
		}
		if(updateLife) UpdateLifeUI();

		UpdateColor();
	}

	private void UpdateLifeUI()
	{
		if (currentLife - (lifeText.fillAmount * 100 / maxFillAmount) > 0.1f && lifeText.fillAmount * 100 / maxFillAmount < currentLife)
			lifeText.fillAmount += Time.deltaTime / downRate;
			
		else if (lifeText.fillAmount * 100 / maxFillAmount - currentLife > 0.1f && lifeText.fillAmount * 100f / maxFillAmount > currentLife)
			lifeText.fillAmount -= Time.deltaTime / downRate;
		else
			updateLife = false;
	}

	public void ChangeType(UI_LifeStyle type){

		switch (type) 
		{
			case UI_LifeStyle.Vertical:
				lifeText.fillMethod = Image.FillMethod.Vertical;
				lifeText.fillOrigin = 0;
				maxFillAmount = 1f;
				lifeText.overrideSprite = sprite360;
				break;
			case UI_LifeStyle.Radial360:
				lifeText.fillMethod = Image.FillMethod.Radial360;
				lifeText.fillOrigin = 2;
				maxFillAmount = 1f;
				break;
			case UI_LifeStyle.Radial270:
				lifeText.fillMethod = Image.FillMethod.Radial360;
				lifeText.fillOrigin = 3;
				maxFillAmount = 0.75f;
				if (currentLife > 75f) lifeText.fillAmount = currentLife * maxFillAmount / 100f;
				break;
			case UI_LifeStyle.Radial180:
				lifeText.fillMethod = Image.FillMethod.Radial180;
				lifeText.fillOrigin = 3;
				lifeText.overrideSprite = sprite180;
				maxFillAmount = 1f;
				lifeText.fillAmount = currentLife / 100f;
				break;
		}
	}

	private void UpdateColor(){
		//Cambio color dependiendo de la vida
		if (lifeText.fillAmount <= 0.3f && lifeText.material.color != LowLife) {
			lifeText.material.color = Color.Lerp (lifeText.material.color, LowLife, colorChangeSpeed * Time.deltaTime);
		} else if (lifeText.fillAmount > 0.3f && lifeText.material.color != FullLife){
			lifeText.material.color = Color.Lerp (lifeText.material.color, FullLife, colorChangeSpeed * Time.deltaTime);
		}
	}


}
