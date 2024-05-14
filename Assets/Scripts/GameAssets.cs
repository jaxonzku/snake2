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
    public Sprite SnakeHeadSprite2;
    public Sprite SnakeBody2;
    public Sprite SnakeHead2;
    public Sprite SnakeHeadOpen2;
    public Sprite SnakeHeadChange2;
    public Sprite Creature;

}
