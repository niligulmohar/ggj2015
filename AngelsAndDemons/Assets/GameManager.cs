using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public CameraScript myCamera;

	public enum GameState {GameWon, Death, GameRunning, GameStarting};
	public GameState myGameState = GameState.GameStarting;

	public static GameManager Instance;

	private int HumanScore_;
	private int DevilScore_;
	private int AngelScore_;

	public AudioSource SoundPlayer;
	public AudioClip StartSound;
	public AudioClip DevilWinSound;
	public AudioClip AngelWinSound;
	public AudioClip HumanWinSound;
	public AudioClip DeathSound;

	public void PlaySound(AudioClip a) {
		SoundPlayer.clip = a;
		SoundPlayer.Play ();
	}

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
				Death();
			myCamera.SetLife(PlayerLife);
		}
	}

	public float LifeRegen;
	public void DamagePlayer() {
		PlayerLife-=1;
	}

	public Slider WonSliderHuman;
	public Slider WonSliderDevil;
	public Slider WonSliderAngel;

	public Animator GameGUIAnimator;

	public void EndGame() {
		if (myGameState!= GameState.GameRunning)
			return;

		myGameState = GameState.GameWon;

		GameGUIAnimator.SetInteger("gamestate",2);

		float maxScore = Mathf.Max(AngelScore, Mathf.Max(DevilScore, HumanScore));
		WonSliderAngel.maxValue = maxScore;
		WonSliderDevil.maxValue = maxScore;
		WonSliderHuman.maxValue = maxScore;

		WonSliderAngel.value = AngelScore;
		WonSliderDevil.value = DevilScore;
		WonSliderHuman.value = HumanScore;

		if (maxScore == HumanScore || AngelScore == DevilScore) {
			PlaySound(HumanWinSound);
		} else if (maxScore == AngelScore) {
			PlaySound(AngelWinSound);
		} else {
			PlaySound(DevilWinSound);
		}

		//Application.LoadLevel(0);
	}

	public void StartGame() {
		myGameState = GameState.GameRunning;
		GameGUIAnimator.SetInteger("gamestate",1);
		PlaySound(StartSound);
	}

	public void Death() {
		if (myGameState!= GameState.GameRunning)
			return;
		GameGUIAnimator.SetInteger("gamestate",3);
		
		myGameState = GameState.Death;
		//
	}

	public void Restart() {
		Application.LoadLevel(0);
	}

	public void RedrawGUI() {
		myHumanText.text = ""+HumanScore;
		myDevilText.text = ""+DevilScore;
		myAngelText.text = ""+AngelScore;
	}

	// Use this for initialization
	void Start () {
		GameGUIAnimator.SetInteger("gamestate",0);
		
		Instance = this;
		PlayerLife = MaxPlayerLife;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerLife += LifeRegen*Time.deltaTime;
		PlayerLife = Mathf.Min(MaxPlayerLife, PlayerLife);
	}
}
