using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipWeapon : MonoBehaviour
{
    public GameObject axeOnBack;
    public GameObject axeOnHand;


    public void AxeChanger()
    {
        axeOnBack.SetActive(false);
        axeOnHand.SetActive(true);
    }


}
