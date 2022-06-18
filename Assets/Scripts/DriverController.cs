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
    [SerializeField] private float timeToWaitSlowdown;
    [SerializeField] private float timeToDeleteBooster;
    [SerializeField] private float timeToDeleteSlowdown;
    [SerializeField] private float boosterTime;
    [SerializeField] private float slowdownTime;

    private FixedJoystick _fixedJoystick;
    private GameObject _booster;
    private GameObject _newBooster;
    private GameObject _slowdown;
    private GameObject _newSlowdown;

    // for mowing
    private float _rotateAmount;
    private float _moveAmount;
    private float _moveSpeed;

    // for boosters
    private float _waitTimeBooster;
    private bool _isBoosting = false;
    private bool _isWaitingBooster = true;
    private float _boosterTime;

    // for slowdowns
    private float _waitTimeSlowdown;
    private bool _isSlow = false;
    private bool _isWaitingSlowdown = true;
    private float _slowdownTime;

    private void Start()
    {
        _moveSpeed = moveSpeed;
        _fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        _booster = GameObject.FindGameObjectWithTag("Boost");
        _slowdown = GameObject.FindGameObjectWithTag("Slow");
        _waitTimeBooster = timeToWaitBooster;
        _waitTimeSlowdown = timeToWaitSlowdown;
        _boosterTime = boosterTime;
        _slowdownTime = slowdownTime;
    }

    void Update()
    {
        _rotateAmount = _fixedJoystick.Horizontal * rotateSpeed * Time.deltaTime;
        _moveAmount = _fixedJoystick.Vertical * moveSpeed * Time.deltaTime;

        //rotate car
        transform.Rotate(0, 0, -_rotateAmount);


        //Booster work
        BoosterRespawn();
        GoWithBooster();

        //Slowdown work
        SlowDownRespawn();
        GoWithSlowdown();

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

    void CreateObjectBooster()
    {
        _newBooster = Instantiate(_booster, new Vector3 (Random.Range(-10,10), Random.Range(-10,10),0), Quaternion.Euler(0,0,0));
    }

    void CreateObjectSlowdown()
    {
        _newSlowdown = Instantiate(_slowdown, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.Euler(0, 0, 0));
    }

    IEnumerator BoostEmergence()
    {
        CreateObjectBooster();
        yield return new WaitForSeconds(timeToDeleteBooster);
        Destroy(_newBooster);
        _isWaitingBooster = true;

    }

    IEnumerator SlowdownEmergence()
    {
        CreateObjectSlowdown();
        yield return new WaitForSeconds(timeToDeleteSlowdown);
        Destroy(_newSlowdown);
        _isWaitingSlowdown = true;

    }

    //Timer for respawn booster
    private void BoosterRespawn()
    {
        if (_isWaitingBooster && !_isBoosting)
        {
            _moveSpeed = moveSpeed;
            _waitTimeBooster -= Time.deltaTime;

          if (_waitTimeBooster < 0f)
          {
            _waitTimeBooster = timeToWaitBooster;
            _isWaitingBooster = false;
            StartCoroutine(BoostEmergence());
          }
        }
    }

    //Timer for respawn slowdown
    private void SlowDownRespawn()
    {
        if (_isWaitingSlowdown && !_isSlow)
        {
            _moveSpeed = moveSpeed;
            _waitTimeSlowdown -= Time.deltaTime;

            if (_waitTimeSlowdown < 0f)
            {
                _waitTimeSlowdown = timeToWaitSlowdown;
                _isWaitingSlowdown = false;
                StartCoroutine(SlowdownEmergence());
            }
        }
    }

    //Timer for working booster (how long car go with high speed)
    private void GoWithBooster()
    {
        if (_isBoosting)
        {
            _moveSpeed = boostSpeed;
            _boosterTime -= Time.deltaTime;
            if (_boosterTime < 0f)
            {
                _boosterTime = boosterTime;
                _isBoosting = false;
            }
        }
        
    }

    //Timer for working slowdown (how long car go with low speed)
    private void GoWithSlowdown()
    {
        if (_isSlow)
        {
            _moveSpeed = slowSpeed;
            _slowdownTime -= Time.deltaTime;
            if (_slowdownTime < 0f)
            {
                _slowdownTime = slowdownTime;
                _isSlow = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boost")
        {
            _moveSpeed = boostSpeed;
            _isBoosting = true;
        }

        if(other.tag == "Slow")
        {
            _moveSpeed = slowSpeed;
            _isSlow = true;
        }
    }

}
