using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector3 position;

    public float stoneOnBuild;
    public float oreOnBuild;
    public float woodOnBuild;
    public float wheatOnBuild;

    public float power;

    public GameObject unitPrefab;

    public void Create(Vector3 position)
    {
        if (IsFreeSpaceNearby(position))
        {
            if (stoneOnBuild > GetPlayerStone() || woodOnBuild > GetPlayerWood() || wheatOnBuild > GetPlayerWheat() || oreOnBuild > GetPlayerOre())
            {
                PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
                pop.popUp("Ошибка создания юнита", "Не хватает ресурсов");
            }
            else
            {
                Instantiate(unitPrefab, position, Quaternion.identity);
            }
        }
        else
        {
            PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
            pop.popUp("Ошибка создания юнита", "Нет свободного места рядом с казармой");
        }

    }

    public float GetPlayerWood()
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

    public bool IsFreeSpaceNearby(Vector3 position)
    {
        return true;
    }
}
