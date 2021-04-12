using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargePixie : MonoBehaviour
{
    Text charge;
    public int chargeNumber;
    public Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        charge = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tag == "Pixie")
        {
            charge.text = Movement.Instance.chargeNumber.ToString();
            movement.chargeNumber++;
        }
    }
}
