using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float timeToWaitBooster;
    [SerializeField] private float timeToDeleteBooster;

    private FixedJoystick _fixedJoystick;
    private GameObject _booster;
    private GameObject _newBooster;
    private float _rotateAmount;
    private float _moveAmount;
    private float _waitTime;
    private bool _isWait = false;
    private bool _isBoosting = false;

   

    private void Start()
    {
        _fixedJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        _booster = GameObject.FindGameObjectWithTag("Boost");
        _waitTime = timeToWaitBooster;
        
    }

    void Update()
    {
        _rotateAmount = _fixedJoystick.Horizontal * rotateSpeed * Time.deltaTime;
        _moveAmount = _fixedJoystick.Vertical * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -_rotateAmount);
    }

    //private void FixedUpdate()
    //{
        /*_rb.velocity = new Vector3(_fixedJoystick.Horizontal * moveSpeed, _fixedJoystick.Vertical * moveSpeed, 0 );
        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            /* _horizontal = _fixedJoystick.Horizontal;
             _rotateAmount = _horizontal * _fixedJoystick.Vertical * rotateSpeed * Time.deltaTime;
             transform.Rotate(0, 0, -_rotateAmount);*/

           // transform.rotation = Quaternion.Euler(0, 0, angle * _fixedJoystick.Horizontal);

        //}

        //transform.rotation = transform.rotation * Quaternion.AngleAxis(_fixedJoystick.Horizontal * rotateSpeed, Vector3.forward);

        //_rb.AddForce(0, 0, runSpeed * Time.deltaTime);
   // }

    public void Moving()
    {
        _moveAmount = moveSpeed * Time.deltaTime;
        transform.Translate(0, _moveAmount, 0);

    }

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
    
    }

    private void BoosterRespawn()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _waitTime = timeToWaitBooster;
            _isWait = false;
            StartCoroutine(BoostEmergence());
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boost")
        {
            moveSpeed = boostSpeed;
            _isBoosting = true;
        }
    }

}
