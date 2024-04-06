using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Vector3 position;

    public float stoneOnBuild;
    public float oreOnBuild;
    public float woodOnBuild;
    public float wheatOnBuild;

    public float stoneIncome;
    public float oreIncome;
    public float woodIncome;
    public float wheatIncome;

    public bool requiresWoods;
    public bool requiresMountain;
    public bool requiresWater;

    public GameObject buildingPrefab;

    public void Build(Vector3 position)
    {
        if ((requiresWoods && CheckWoodsConds(position)) || (requiresMountain && CheckWaterConds(position)) || (requiresWoods && CheckMountainConds(position)))
        {
            if(stoneOnBuild > GetPlayerStone() || woodOnBuild > GetPlayerWood() || wheatOnBuild > GetPlayerWheat() || oreOnBuild > GetPlayerOre())
            {
                PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
                pop.popUp("Ошибка постройки", "Не хватает ресурсов");
            }
            else
            {
                Instantiate(buildingPrefab, position, Quaternion.identity);
            }

        }
        else
        {
            PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
            pop.popUp("Ошибка постройки", "Не подходящая позиция");
        }
    }

    public bool CheckWaterConds(Vector3 position)
    {
        return true;
    }
    public bool CheckWoodsConds(Vector3 position)
    {
        return true;
    }
    public bool CheckMountainConds(Vector3 position)
    {
        return true;
    }
    public float GetPlayerWood ()
    {
        return 1.19f;
    }
    public float GetPlayerStone()
    {
        return 1.19f;
    }
    public float GetPlayerOre()
    {
        return 1.19f;
    }
    public float GetPlayerWheat()
    {
        return 1.19f;
    }
}
