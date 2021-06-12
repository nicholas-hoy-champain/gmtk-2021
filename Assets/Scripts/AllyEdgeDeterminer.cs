using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyEdgeDeterminer : MonoBehaviour
{
    public bool top = false;
    public bool bottom = false;
    public bool left = false;
    public bool right = false;
    bool center { get { return !(top || bottom || left || right); } }
    public bool displayBoolCenter;

    [SerializeField] int count, index, layer, totalLayers, filledLayers, endIndexOfLayer;

    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEdge();
        displayBoolCenter = center;
    }

    public void CheckForEdge()
    {
        count = transform.parent.childCount;
        index = transform.GetSiblingIndex();

        layer = 1;
        int i = 8;
        while(index >= i)
        {
            layer++;
            i += 8 * layer;
        }

        totalLayers = 1;
        i = 8;
        while (count > i)
        {
            totalLayers++;
            i += 8 * totalLayers;
        }

        if(i == count)
        {
            filledLayers = totalLayers;
        }
        else
        {
            filledLayers = totalLayers - 1;
        }

        int dir = index % 4;

        endIndexOfLayer = 0;
        for (i = 1; i <= layer; i++)
        {
            endIndexOfLayer += 8 * i;
        }
        endIndexOfLayer--;

        if (layer < filledLayers || index + (layer * 8) < count)
        {
            top = false;
            bottom = false;
            left = false;
            right = false;

            if(layer == filledLayers && index + 4 > endIndexOfLayer) //is corner of the last filled layer
            {
                int test = 0;

                int diff = endIndexOfLayer - index;
                int nextEnd = endIndexOfLayer + ((1 + layer) * 8);

                if (diff == 3)
                    diff = 0;
                else if (diff == 1)
                    diff = 2;
                else if (diff == 2)
                    diff = 3;
                else if (diff == 0)
                    diff = 1;

                test = nextEnd - diff - 4;

                //Debug.Log("Data: " + index + " " + endIndexOfLayer + " " + diff + " " + nextEnd + " " + test + " " + count);

                if (test >= count) //Means the corner has an uncovered side, and it adds the bool
                {

                    //Debug.LogError("Success: " + index);

                    switch (dir)
                    {
                        case 0:
                            right = true;
                            break;
                        case 1:
                            top = true;
                            break;
                        case 2:
                            left = true;
                            break;
                        case 3:
                            bottom = true;
                            break;
                    }
                }
            }

            return;
        }


        switch (dir)
        {
            case 0:
                top = true;
                if (index + 4 > endIndexOfLayer)
                {
                    right = true;
                }
                else
                {
                    if (!FindOffset(1, 0))
                    {
                        right = true;
                    }
                    if (!FindOffset(-1, 0))
                    {
                        left = true;
                    }
                }
                break;
            case 1:
                left = true;
                if (index + 4 > endIndexOfLayer)
                {
                    top = true;
                }
                else
                {
                    if (!FindOffset(0, 1))
                    {
                        top = true;
                    }
                    if (!FindOffset(0, -1))
                    {
                        bottom = true;
                    }
                }
                break;
            case 3:
                right = true;
                if (index + 4 > endIndexOfLayer)
                {
                    bottom = true;
                }
                else
                {
                    if (!FindOffset(0, 1))
                    {
                        top = true;
                    }
                    if (!FindOffset(0, -1))
                    {
                        bottom = true;
                    }
                }
                break;
            case 2:
                bottom = true;
                if (index + 4 > endIndexOfLayer)
                { 
                    left = true; 
                }
                else
                {
                    if (!FindOffset(1, 0))
                    {
                        right = true;
                    }
                    if (!FindOffset(-1, 0))
                    {
                        left = true;
                    }
                }
                break;
        }
    }

    bool FindOffset(int x, int y)
    {
        List<Vector3> offsets = gameManager.GetComponent<GameManager>().offsets;
        float size = gameManager.GetComponent<GameManager>().size;
        Vector3 ideal = offsets[transform.GetSiblingIndex()] + new Vector3(x,y)*size;
        for(int i = endIndexOfLayer + 1 - layer * 8; i < count; i++)
        {
            if (offsets[i] == ideal) return true;
        }
        return false;
    }
}
