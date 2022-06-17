using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    [Header("Driver velocity")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float boostSpeed;

    [Header("Timers")]
    [SerializeField] private float timeToWaitBooster;
    [SerializeField] private float timeToDeleteBooster;
    [SerializeField] private float boosterTime;

    private FixedJoystick _fixedJoystick;
    private GameObject _booster;
    private GameObject _newBooster;

    private float _rotateAmount;
    private float _moveAmount;
    private float _moveSpeed;
    private float _waitTime;
    private bool _isBoosting = false;
    private bool _isWaiting = true;
    private float _boosterTime;

   

    private void Start()
    {
        _moveSpeed = moveSpeed;
        _fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        _booster = GameObject.FindGameObjectWithTag("Boost");
        _waitTime = timeToWaitBooster;
        _boosterTime = boosterTime;
    }

    void Update()
    {
        _rotateAmount = _fixedJoystick.Horizontal * rotateSpeed * Time.deltaTime;
        _moveAmount = _fixedJoystick.Vertical * moveSpeed * Time.deltaTime;

        //rotate car
        transform.Rotate(0, 0, -_rotateAmount);

        //Booster work
        if (_isWaiting && !_isBoosting)
        {
            _moveSpeed = moveSpeed;
            BoosterRespawn();
        }
        else if (_isBoosting)
        {
            GoWithBooster();
        }

    }

    //Method for drive car with button (MoveController)
    public void Moving()
    {
        _moveAmount = _moveSpeed * Time.deltaTime;
        transform.Translate(0, _moveAmount, 0);

    }

    //Method for stop car with button (MoveController)
    public void Stopping()
    {
        transform.Translate(0, 0, 0);
    }

    void CreateObject()
    {
        _newBooster = Instantiate(_booster, new Vector3 (Random.Range(-10,10), Random.Range(-10,10),0), Quaternion.Euler(0,0,0));
    }

    IEnumerator BoostEmergence()
    {
        CreateObject();
        yield return new WaitForSeconds(timeToDeleteBooster);
        Destroy(_newBooster);
        _isWaiting = true;

    }

    //Timer for respawn booster
    private void BoosterRespawn()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _waitTime = timeToWaitBooster;
            _isWaiting = false;
            StartCoroutine(BoostEmergence());
        }
    }

    //Timer for working booster (how long car go with high speed)
    private void GoWithBooster()
    {
        _boosterTime -= Time.deltaTime;
        if (_boosterTime < 0f)
        {
            _boosterTime = boosterTime;
            _isBoosting = false;
            Debug.Log("Boosting finish");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boost")
        {
            _moveSpeed = boostSpeed;
            _isBoosting = true;
        }
    }

}
