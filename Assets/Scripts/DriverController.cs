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
    [SerializeField] private float timeToWaitCatch;
    [SerializeField] private float timeToDeleteBooster;
    [SerializeField] private float timeToDeleteCatch;
    [SerializeField] private float boosterTime;
    [SerializeField] private float slowdownTime;

    [Header("Positions")]
    [SerializeField] private List<Vector3> boosterPosition;
    [SerializeField] private List<Vector3> catchPosition;

    [Header("Catch")]
    [SerializeField] private int CatchInLevel;

    private FixedJoystick _fixedJoystick;
    private GameObject _booster;
    private GameObject _newBooster;
    private GameObject _catch;
    private GameObject _newCatch;


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
    private bool _isSlow = false;
    private float _slowdownTime;


    //for catch
    private bool _isCatch = false;
    private int _catchCount = 0;
    private bool _isFull;

    private void Start()
    {
        _moveSpeed = moveSpeed;
        _fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        _booster = GameObject.FindGameObjectWithTag("Boost");
        _catch = GameObject.FindGameObjectWithTag("Catch");
        _waitTimeBooster = timeToWaitBooster;
        _boosterTime = boosterTime;
        _slowdownTime = slowdownTime;

        CreateObjectCatch();

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
        GoWithSlowdown();

        //CatchWork
        CatchRespawn();

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
        _newBooster = Instantiate(_booster, boosterPosition[Random.Range(0, boosterPosition.Count)], Quaternion.Euler(0,0,0));
    }

    void CreateObjectCatch()
    {
        _newCatch = Instantiate(_catch, catchPosition[Random.Range(0, catchPosition.Count)], Quaternion.Euler(0, 0, 0));
        //for (int i = 0; i < catchPosition.Count; i++)
        //{
            //_newCatch = Instantiate(_catch, catchPosition[i], Quaternion.Euler(0, 0, 0));
        //}
    }

    IEnumerator BoostEmergence()
    {
        CreateObjectBooster();
        yield return new WaitForSeconds(timeToDeleteBooster);
        Destroy(_newBooster);
        _isWaitingBooster = true;

    }

    IEnumerator CatchEmergence()
    {
        Destroy(_newCatch);
        _isCatch = false;
        if (!_isFull)
        {
            yield return new WaitForSeconds(timeToDeleteCatch);
            CreateObjectCatch();
        }

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


    //Timer for respawn catch
    private void CatchRespawn()
    {
        if (_isCatch)
        {
            StartCoroutine(CatchEmergence());
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
        else if (_isSlow)
        {
            _moveSpeed = moveSpeed;
        }

    }

    private void CountCatch()
    {
        if (!_isFull)
        {
            _catchCount++;
            Debug.Log(_catchCount);
        }


        if(_catchCount == CatchInLevel)
        {
            _isFull = true;
        }
    }

    public int GetCountCatch()
    {
        return _catchCount;
    }

    public int GetCatchInLevel()
    {
        return CatchInLevel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boost")
        {
            Destroy(_newBooster);
            _moveSpeed = boostSpeed;
            _isBoosting = true;
        }

        if(other.tag == "Slow")
        {
            _moveSpeed = slowSpeed;
            _isSlow = true;
        }

        if(other.tag == "Catch")
        {
            _isCatch = true;
            CountCatch();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isSlow = true;
    }

}
