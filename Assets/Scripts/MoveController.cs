using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    DriverController _driverController;

    bool _go = false;

    private void Start()
    {
        GameObject driverObject = GameObject.FindGameObjectWithTag("Driver");
        _driverController = driverObject.GetComponent<DriverController>();
    }
    private void Update()
    {
        if(_go == true)
        {
            _driverController.Moving();
        }
        else
        {
            _driverController.Stopping();
        }
        
    }

    public void Go()
    {
        _go = true;
    }

    public void Stop()
    {
        _go = false;
    }
}
