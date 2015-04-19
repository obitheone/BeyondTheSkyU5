using UnityEngine;
using System.Collections;

public class TP_Status : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Status Instance;
    public Animator animController;
    public GameObject lifeHUD;



    //PRIVATE
	private bool _isSinking;
    private int _vida;
    private bool _isDead;
    private bool _isMoving;
    private bool _isJumping;
    private bool _isRejumping;
    private bool _isTargetting;
    private bool _isControllable;
	private int _ground;
    private float _movingBlend;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        _vida = 100;
        _isDead = _isSinking = _isJumping = _isRejumping = false;
        _isControllable = true;
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

        lifeHUD.GetComponent<UI_LifeController>().SetLife((float)_vida);
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

        lifeHUD.GetComponent<UI_LifeController>().SetLife((float)_vida);
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
    public bool IsMoving() { return _isMoving; }
    public float MovingBlend()
    {
        return _movingBlend;
    }

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

    public void SetMoving(bool value, float blend)
    {
        _isMoving = value;
        if(value)_movingBlend = blend;
    }

	// Update is called once per frame
	void Update () {
		if(_isSinking)
        {
			TP_Skills.Instance.player.transform.Translate (-Vector3.up * 0.25f * Time.deltaTime);
		}

        if (_isMoving)
        {
            //inicio animacion
            animController.SetBool("isMoving", true);
            //estoy andando o corriendo
            animController.SetFloat("MovingBlend", _movingBlend);
        }
        else animController.SetBool("isMoving", false);

        if (_isJumping || _isRejumping)
        {
            animController.SetBool("isJumping", true);
        }
        else
        {
            animController.SetBool("isJumping", false);
            animController.SetBool("isGrounded", true);
        }
	}

    public bool IsControllable()
    {
        return _isControllable;
    }

    public void SetControllable(bool value)
    {
        _isControllable = value;
    }
	
	IEnumerator CargarEscena( float t ) {
		yield return new WaitForSeconds( t );
		Application.LoadLevel(Application.loadedLevel);
	}
}
