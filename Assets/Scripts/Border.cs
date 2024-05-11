using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    public Sprite singleCell;

    void Start()
    {

        for (int i = -37; i < 37; i++)
        {
            generateBorder(i, 27);

        }

        // Debug.Log("setup done");
    }

    void Update()
    {

    }
    void FixedUpdate()
    {

    }


    void generateBorder(int x, int y)
    {
        GameObject borderCell = new GameObject("BoredrCell");
        SpriteRenderer cellRenderer = borderCell.AddComponent<SpriteRenderer>();
        cellRenderer.sprite = singleCell;
        borderCell.transform.position = new Vector2(x, y);
    }
}
