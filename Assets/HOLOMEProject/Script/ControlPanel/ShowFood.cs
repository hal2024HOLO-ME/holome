using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFood : MonoBehaviour
{
    public GameObject Food;
    public GameObject FoodTable;
    public void OnClickFoodButton()
    {
        if(Food.activeSelf)
        {
            Food.SetActive(false);
            FoodTable.SetActive(false);
            return;
        }
        Food.SetActive(true);
        FoodTable.SetActive(true);
    }
}
