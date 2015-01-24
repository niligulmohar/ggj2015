using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public CameraScript myCamera;

	public enum GameState {GameWon, Death, GameRunning};
	public GameState myGameState = GameState.GameRunning;

	public static GameManager Instance;

	private int HumanScore_;
	private int DevilScore_;
	private int AngelScore_;

	public int HumanScore {
		get {
			return HumanScore_;
		}
		set {
			HumanScore_ = value;
			RedrawGUI();
		}
	}
	public int DevilScore{
		get {
			return DevilScore_;
		}
		set {
			DevilScore_ = value;
			RedrawGUI();
		}
	}
	public int AngelScore{
		get {
			return AngelScore_;
		}
		set {
			AngelScore_ = value;
			RedrawGUI();
		}
	}

	public Text myHumanText;
	public Text myDevilText;
	public Text myAngelText;

	public float MaxPlayerLife;
	private float playerLife_;
	private float playerLife {
		get {
			return playerLife_;
		}
		set {
			playerLife_ = value;
			if (playerLife<=0)
				EndGame();
			myCamera.SetLife(playerLife);
		}
	}

	public float LifeRegen;
	public void DamagePlayer() {
		playerLife-=1;
	}

	public void EndGame() {
		myGameState = GameState.GameWon;
		Application.LoadLevel(0);
	}

	public void Death() {
		myGameState = GameState.Death;
		Application.LoadLevel(0);
	}

	public void RedrawGUI() {
		myHumanText.text = ""+HumanScore;
		myDevilText.text = ""+DevilScore;
		myAngelText.text = ""+AngelScore;
	}

	// Use this for initialization
	void Start () {
		Instance = this;
		playerLife = MaxPlayerLife;
	}
	
	// Update is called once per frame
	void Update () {
		playerLife += LifeRegen*Time.deltaTime;
		playerLife = Mathf.Min(MaxPlayerLife, playerLife);
	}
}
