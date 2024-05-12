using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;
    private void Awake()
    {
        i = this;
    }
    public Sprite SnakeHeadSprite;
    public Sprite foodsprite;
    public Sprite SnakeBody;
    public Sprite SnakeHead;
    public Sprite SnakeHeadOpen;
    public Sprite SnakeHeadChange;



}
