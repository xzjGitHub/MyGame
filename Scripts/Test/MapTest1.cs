﻿using UnityEngine;
using System.Collections;


public class MapTest1 : MonoBehaviour
{
    public int row = 30;
    public int col = 35;
    private bool[,] mapArray;
    public GameObject cube1, cube2;
    GameObject cubes;
    int times;
    int a;


    void Start()
    {
        cubes = new GameObject();
        mapArray = InitMapArray();
        CreateMap(mapArray);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Button1();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Button2();
        }
    }


    bool[,] InitMapArray()
    {
        bool[,] array = new bool[row, col];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                array[i, j] = Random.Range(0, 100) < 60;
                if (i == 0 || i == row - 1 || j == 0 || j == col - 1)
                {
                    array[i, j] = false;
                }
            }
        }


        return array;
    }


    bool[,] SmoothMapArray(bool[,] array)
    {
        bool[,] newArray = new bool[row, col];
        int count1, count2;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                count1 = CheckNeighborWalls(array, i, j, 1);
                count2 = CheckNeighborWalls(array, i, j, 2);


                if (count1 >= 5 || count2 <= 2)
                {
                    newArray[i, j] = false;
                }
                else
                {
                    newArray[i, j] = true;
                }


                if (i == 0 || i == row - 1 || j == 0 || j == col - 1)
                {
                    newArray[i, j] = false;
                }


                // newArray[i, j] = count1 >= 5 || count2 <= 2 ? true : false;
            }
        }
        return newArray;
    }


    int CheckNeighborWalls(bool[,] array, int i, int j, int t)
    {
        int count = 0;


        for (int i2 = i - t; i2 < i + t + 1; i2++)
        {
            for (int j2 = j - t; j2 < j + t + 1; j2++)
            {
                if (i2 > 0 && i2 < row && j2 >= 0 && j2 < col)
                {
                    if (!array[i2, j2])
                    {
                        count++;
                    }
                }
            }
        }
        if (!array[i, j])
            count--;
        return count;
    }


    void CreateMap(bool[,] array)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (!array[i, j])
                {
                    GameObject go = Instantiate(cube1, new Vector3(i, 1, j), Quaternion.identity) as GameObject;
                    go.transform.SetParent(cubes.transform);
                }
                else
                {
                    GameObject go = Instantiate(cube2, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                    go.transform.SetParent(cubes.transform);
                }
            }
        }
    }


    public void Button1()
    {
        times = 0;
        Destroy(cubes);
        cubes = new GameObject();
        mapArray = InitMapArray();
        CreateMap(mapArray);
    }


    public void Button2()
    {
        if (times < 7)
        {
            times++;
            Destroy(cubes);
            cubes = new GameObject();
            mapArray = SmoothMapArray(mapArray);
            CreateMap(mapArray);
        }
    }
}
