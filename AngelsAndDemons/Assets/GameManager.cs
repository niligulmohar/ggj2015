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
	public float PlayerLife	 {
		get {
			return playerLife_;
		}
		set {
			playerLife_ = value;
			if (PlayerLife<=0)
				EndGame();
			myCamera.SetLife(PlayerLife);
		}
	}

	public float LifeRegen;
	public void DamagePlayer() {
		PlayerLife-=1;
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
		PlayerLife = MaxPlayerLife;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLife += LifeRegen*Time.deltaTime;
		PlayerLife = Mathf.Min(MaxPlayerLife, PlayerLife);
	}
}
