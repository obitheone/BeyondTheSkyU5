using UnityEngine;
using System.Collections;

public class TP_Status : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Status Instance;



    //PRIVATE
	private bool _isSinking;
    private int _vida;
    private bool _isDead;
    private bool _isJumping;
    private bool _isRejumping;
    private bool _isTargetting;
	private int _ground;

    void Awake()
    {
        Instance = this;
        _isJumping = _isRejumping = false;
    }

	// Use this for initialization
	void Start () {
        _vida = 100;
        _isDead = false;
		_isSinking = false;
	}

    public int GetVida(){ return _vida; }

    public void SetVida(int num)
    {
        if (num == 0)
        {
            Debug.LogError("No se puede asignar vida 0");
            return;
        } else if (num <= 100) _vida = num;
    }

    public void AddVida(int num)
    {
        if (_vida + num > 100) _vida = 100;
        else _vida += num;
    }

    public void SubsVida(int num)
    {
        if (_vida - num > 0)
        {
            _vida -= num;
        }
        else
        {
            _vida = 0;
            _isDead = true;
            OnDeath();
        }
    }

    public void OnDeath()
    {
        //do something on Death
        _isSinking = true;
        StartCoroutine(CargarEscena(0.5f));
    }

    public bool IsJumping() { return _isJumping; }
    public bool IsReJumping() { return _isRejumping; }
    public bool IsTargetting() { return _isTargetting; }
    public bool IsDead() { return _isDead; }

    public void SetJumping(bool value)
    {
        _isJumping = value;
    }

    public void SetReJumping(bool value)
    {
        _isRejumping = value;
    }

    public void SetTargetting(bool value)
    {
        _isTargetting = value;
    }

	public void SetGround(int ground){
		_ground=ground;
	}

	public int GetGround(){
		return _ground;
	}
	// Update is called once per frame
	void Update () {
		if(_isSinking)
        {
			TP_Skills.Instance.player.transform.Translate (-Vector3.up * 0.25f * Time.deltaTime);
		}
	}
	
	IEnumerator CargarEscena( float t ) {
		yield return new WaitForSeconds( t );
		Application.LoadLevel(Application.loadedLevel);
	}
}
