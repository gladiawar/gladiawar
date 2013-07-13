using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	
	private static float _BASE = 32;
	private static float _FONT = _BASE - 2;
	
	private static float _EFFECT_WIDTH = _BASE * 12;
	private static float _EFFECT_HEIGHT = _BASE;
	private static float _LEVEL_WIDTH = _BASE;
	private static float _LEVEL_HEIGHT = _LEVEL_WIDTH;
	private static float _LIFE_WIDTH = _BASE * 8;
	private static float _LIFE_HEIGHT = _BASE;
	private static float _NAME_WIDTH = _BASE * 8;
	private static float _NAME_HEIGHT = _BASE;
	private static float _PICTURE_WIDTH = _BASE * 3;
	private static float _PICTURE_HEIGHT = _PICTURE_WIDTH;
	private static float _WEAPON_WIDTH = _BASE;
	private static float _WEAPON_HEIGHT = _WEAPON_WIDTH;
	
	public GameObject Player {get; set; }
	
	public GameObject Effect;
	public GameObject Level;
	public GameObject Life;
	public GameObject Name;
	public GameObject Picture;
	public GameObject Weapon;
	
	// Use this for initialization
	void Start () {
//		this.StartEffect();
		this.StartLevel();
		this.StartLife();
		this.StartName();
		this.StartPicture();
//		this.StartWeapon();
		
		
		this.Effect.transform.localPosition = new Vector3(0f, -(_PICTURE_HEIGHT), 0f);
		this.Effect.transform.localScale = new Vector3(_EFFECT_WIDTH, _EFFECT_HEIGHT, 0.01f);
		
		this.Weapon.transform.localPosition = new Vector3(0, -(_PICTURE_HEIGHT - _WEAPON_HEIGHT), 0f);
		this.Weapon.transform.localScale = new Vector3(_WEAPON_WIDTH, _WEAPON_HEIGHT, 0.01f);
	}
	
	void StartLevel() {
		var background = this.Level.transform.FindChild("Background");
		var data = this.Level.transform.FindChild("Data");
		var label = data.GetComponent<UILabel>();
		
		this.Level.transform.localPosition = new Vector3(0f, 0f, 0f);
		background.localScale = new Vector3(_LEVEL_WIDTH, _LEVEL_HEIGHT, 0.01f);
		data.localScale = new Vector3(_FONT, _FONT, 0.01f);
		
		label.text = "80";
	}
	
	void StartLife() {
		var background = this.Life.transform.FindChild("Background");
		var life = this.Life.transform.FindChild("Life");
		var lost = this.Life.transform.FindChild("Lost");
		
		this.Life.transform.localPosition = new Vector3(_LEVEL_WIDTH + _PICTURE_WIDTH, -(_NAME_HEIGHT), 0f);
		
		background.localScale = new Vector3(_LIFE_WIDTH, _LIFE_HEIGHT, 0.01f);
	}
	
	void StartName() {
		var background = this.Name.transform.FindChild("Background");
		var data = this.Name.transform.FindChild("Data");
		var label = data.GetComponent<UILabel>();
		
		this.Name.transform.localPosition = new Vector3(_LEVEL_WIDTH + _PICTURE_WIDTH, 0f, 0f);
		data.localPosition = new Vector3(10, 0f, 0f);
		
		background.localScale = new Vector3(_NAME_WIDTH, _NAME_HEIGHT, 0.01f);
		data.localScale = new Vector3(_FONT, _FONT, 0.01f);
		
		label.text = "I'm a fat nigger";
	}
	
	void StartPicture() {
		var background = this.Picture.transform.FindChild("Background");
		var data = this.Picture.transform.FindChild("Data");
		
		this.Picture.transform.localPosition = new Vector3(_LEVEL_WIDTH, 0f, 0f);
		
		background.localScale = new Vector3(_PICTURE_WIDTH, _PICTURE_HEIGHT, 0.01f);
		data.localScale = new Vector3(_PICTURE_WIDTH, _PICTURE_HEIGHT, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
