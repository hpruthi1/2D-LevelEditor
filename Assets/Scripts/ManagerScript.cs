﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public Mouse user;
    public SpriteRenderer spriteRenderer;
    private LevelEditor level;
    public GameObject Platform;
    public GameObject Coin;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        CreateEditor(); // create new instance of level.
    }

    LevelEditor CreateEditor()
    {
        level = new LevelEditor();
        level.editorObjects = new List<EditorObject.Data>(); // make new list of editor object data.
        return level;
    }

    /// <summary>
    /// Choosing an object
    /// </summary>
    public void ChoosePlatform()
    {
        user.itemOption = Mouse.ItemList.Platform; // set object to place as Platform
        spriteRenderer.sprite = Platform.GetComponent<SpriteRenderer>().sprite;
    }
    
    public void ChooseCoin()
    {
        user.itemOption = Mouse.ItemList.Coin; // set object to place as coin
        spriteRenderer.sprite = Coin.GetComponent<SpriteRenderer>().sprite;
    }

    public void ChoosePlayerStart()
    {
        user.itemOption = Mouse.ItemList.Player; // set object to place as player marker
        spriteRenderer.sprite = Player.GetComponent<SpriteRenderer>().sprite;
    }

}
