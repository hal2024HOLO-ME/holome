using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFood : MonoBehaviour
{
    public GameObject Food;
    public GameObject FoodTable;
    public void OnClickFoodButton()
    {
        Food.SetActive(true);
        FoodTable.SetActive(true);
    }
}
