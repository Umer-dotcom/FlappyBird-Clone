using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] bool redBird = false;
    [SerializeField] bool blueBird = true;
    [SerializeField] bool yellowBird = false;

    [SerializeField] GameObject redTick;
    [SerializeField] GameObject blueTick;
    [SerializeField] GameObject yellowTick;

    [SerializeField] string equippedBird;

    private void Start()
    {
        equippedBird = PlayerPrefs.GetString("Bird");
        EquipBird(equippedBird);
    }

    public void EquipBird(string colour)
    {
        if(colour == "Red")
        {
            redBird = true;
            blueBird = false;
            yellowBird = false;

            PlayerPrefs.SetString("Bird", "Red");

            redTick.SetActive(true);
            blueTick.SetActive(false);
            yellowTick.SetActive(false);
        }
        else if(colour == "Blue")
        {
            redBird = false;
            blueBird = true;
            yellowBird = false;

            PlayerPrefs.SetString("Bird", "Blue");

            redTick.SetActive(false);
            blueTick.SetActive(true);
            yellowTick.SetActive(false);
        }
        else if(colour == "Yellow")
        {
            redBird = false;
            blueBird = false;
            yellowBird = true;

            PlayerPrefs.SetString("Bird", "Yellow");

            redTick.SetActive(false);
            blueTick.SetActive(false);
            yellowTick.SetActive(true);
        }
    }
}
