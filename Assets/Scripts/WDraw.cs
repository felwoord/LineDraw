using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WDraw : MonoBehaviour
{
    private LineRenderer[] lineRend = new LineRenderer[4];
    private Vector3[,] pos = new Vector3[4, 2];
    Vector3 auxPos;
    private bool[] step = new bool[5];
    private float counter;
    private int aux;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            lineRend[i] = GameObject.Find("WDraw" + i).GetComponent<LineRenderer>();
            step[i] = false;
            if (i == 3)
            {
                step[i + 1] = false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                pos[i, j] = lineRend[i].GetPosition(j);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                lineRend[i].SetPosition(j, pos[0, 0]);
            }
        }
        auxPos = pos[0, 0];

        step[0] = true;
    }


    void Update()
    {
        for (aux = 0; aux < 4; aux++)
        {
            if (step[aux] == true)
            {
                if (Vector3.Distance(auxPos, pos[aux, 1]) < 0.01f)
                {
                    step[aux] = false;
                    step[aux + 1] = true;
                }
            }
        }


        if (step[0])
        {

            auxPos = Vector3.Lerp(auxPos, pos[0, 1], 0.15f);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 0 && j == 0) { }
                    else
                    {
                        lineRend[i].SetPosition(j, auxPos);
                    }
                }
            }

        }
        if (step[1])
        {

            auxPos = Vector3.Lerp(auxPos, pos[1, 1], 0.175f);
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 1 && j == 0) { }
                    else
                    {
                        lineRend[i].SetPosition(j, auxPos);
                    }
                }
            }

        }
        if (step[2])
        {

            auxPos = Vector3.Lerp(auxPos, pos[2, 1], 0.2f);
            for (int i = 2; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 2 && j == 0) { }
                    else
                    {
                        lineRend[i].SetPosition(j, auxPos);
                    }
                }
            }

        }
        if (step[3])
        {

            auxPos = Vector3.Lerp(auxPos, pos[3, 1], 0.225f);
            for (int i = 3; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 3 && j == 0) { }
                    else
                    {
                        lineRend[i].SetPosition(j, auxPos);
                    }
                }
            }

        }
        if (step[4])
        {
            counter += Time.deltaTime;
            if (counter > 1.0f)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        lineRend[i].SetPosition(j, pos[0, 0]);
                    }
                }

                auxPos = pos[0, 0];
                counter = 0;
                step[4] = false;
                step[0] = true;
            }
        }

    }
}