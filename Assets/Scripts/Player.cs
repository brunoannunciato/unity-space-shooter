using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private int _life = 3;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    private int _score = 0;
    private float _canFire = -1;
    private float _cooldown = 0.3f;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActived = false;
    private bool _isShieldActived = false;
    private UIManager _uiManager;
    void Start() 
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null) {
            Debug.LogError("Spawn manager is equal null!");
        }

        if (_uiManager == null)
        {
            Debug.LogError("_uiManager is equal null!");
        }
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space)  && Time.time > _canFire) {
            _canFire = Time.time + _cooldown;
            Fire();
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (xPos > 11.27)
        {
            transform.position = new Vector3(-11.27f, transform.position.y, 0);
        }
        else if (xPos < -11.27)
        {
            transform.position = new Vector3(11.27f, transform.position.y, 0);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.36f, 3.36f), 0);
    }

    void Fire()
    {
        Vector3 laserStartPos = new Vector3(transform.position.x, transform.position.y + 1, 0);

        if (_isTripleShotActived == true)
        {
            Instantiate(_tripleShot, new Vector3(transform.position.x, transform.position.y + 0.1f, 0), Quaternion.identity);
        } 
        else
        {
            Instantiate(_laser, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldActived == true)
        {
            DamageShield();
            return;
        }

        _life--;

        if (_life == 2)
        {
            _leftEngine.SetActive(true);
        } else if (_life == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_life);

        if (_life <= 0) {
            Destroy(this.gameObject);
            _spawnManager.StopRespawn();
            _uiManager.ActiveGameOverOverlay();
        }
    }

    public void ActiveTripleShot()
    {
        _isTripleShotActived = true;
        StartCoroutine(PowerDownTripleShot());
    }

    IEnumerator PowerDownTripleShot()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActived = false;
    }

    public void ActiveSpeedUp()
    {
        _speed = 16;
        StartCoroutine(SpeedDown());
    }

    IEnumerator SpeedDown()
    {
        yield return new WaitForSeconds(5);
        _speed = 8;
    }

    public void ActivateShield()
    {
        _isShieldActived = true;
        _shield.SetActive(true);
    }

    private void DamageShield()
    {
        _isShieldActived = false;
        _shield.SetActive(false);
    }

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _uiManager.UpdateScore(_score);
    }
}
