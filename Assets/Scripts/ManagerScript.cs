﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public Mouse User;
    public InputField SaveLevelName;
    public InputField LoadlevelName;
    public Text SaveLoadMessage;
    public SpriteRenderer spriteRenderer;
    public GameObject MessagePopup;
    private LevelEditor level;
    public GameObject Platform;
    public GameObject Coin;
    public GameObject Player;
    public GameObject StartPos;
    public GameObject EndPos;
    [HideInInspector]
    public bool StartpointPresent = false;
    [HideInInspector]
    public bool PlayerPlaced = false;
    [HideInInspector]
    public bool EndPointPlaced = false;


    // Start is called before the first frame update
    void Start()
    {
        MessagePopup.SetActive(false);
        CreateEditor();
    }

    LevelEditor CreateEditor()
    {
        level = new LevelEditor();
        level.editorObjects = new List<EditorObject.Data>();
        return level;
    }

    public void ChoosePlatform()
    {
        User.itemOption = Mouse.ItemList.Platform; // set object to place as Platform
        spriteRenderer.sprite = Platform.GetComponent<SpriteRenderer>().sprite;
    }
    
    public void ChooseCoin()
    {
        User.itemOption = Mouse.ItemList.Coin; // set object to place as coin
        spriteRenderer.sprite = Coin.GetComponent<SpriteRenderer>().sprite;
    }

    public void ChoosePlayerStart()
    {
        User.itemOption = Mouse.ItemList.Player; // set object to place as player
        spriteRenderer.sprite = Player.GetComponent<SpriteRenderer>().sprite;
    }

    public void ChooseStartPoint()
    {
        User.itemOption = Mouse.ItemList.StartPos; // set object to place as startpoint
        spriteRenderer.sprite = StartPos.GetComponent<SpriteRenderer>().sprite;
    }

    public void ChooseEndPoint()
    {
        User.itemOption = Mouse.ItemList.EndPos; // set object to place as endpoint
        spriteRenderer.sprite = EndPos.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnContinueButton()
    {
        MessagePopup.SetActive(false);
    }

    public void SaveLevel()
    {
        EditorObject[] Objectsfound = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in Objectsfound)
            level.editorObjects.Add(obj.data);

        string json = JsonUtility.ToJson(level);
        string folder = Application.dataPath + "/LevelData";
        string levelFile = "";

        if (SaveLevelName.text == "")
            levelFile = "new_level.json";
        else
            levelFile = SaveLevelName.text + ".json";
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        string path = Path.Combine(folder, levelFile);

        if (File.Exists(path))
            File.Delete(path);
        File.WriteAllText(path, json);
        SaveLevelName.text = "";
        SaveLevelName.DeactivateInputField();
        SaveLoadMessage.text = levelFile + " Saved to LevelData folder.";
    }

    public void LoadLevel()
    {
        string folder = Application.dataPath + "/LevelData";
        string levelfile = "";
        if (LoadlevelName.text == "")
            levelfile = "new_level.json";
        else
            levelfile = LoadlevelName.text + ".json";

        string path = Path.Combine(folder, levelfile);

        if (File.Exists(path))
        {
            EditorObject[] Objectsfound = FindObjectsOfType<EditorObject>();
            foreach (EditorObject obj in Objectsfound)
                Destroy(obj.gameObject);
            PlayerPlaced = false;

            string json = File.ReadAllText(path);
            level = JsonUtility.FromJson<LevelEditor>(json);
            CreateFromFile();
        }
        else
        {
            SaveLoadMessage.text = levelfile + " Could not be found !";
            LoadlevelName.DeactivateInputField();
        }
    }

    void CreateFromFile()
    {
        GameObject NewObject;
        for(int i = 0; i<level.editorObjects.Count; i++)
        {
            if(level.editorObjects[i].objectType == EditorObject.ObjectType.Platform)
            {
                NewObject = Instantiate(Platform, level.editorObjects[i].pos, Quaternion.identity);
                NewObject.layer = 9;

                EditorObject eo = NewObject.AddComponent<EditorObject>();
                eo.data.pos = NewObject.transform.position;
                eo.data.objectType = EditorObject.ObjectType.Platform;
            }

            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Coin)
            {
                NewObject = Instantiate(Coin, level.editorObjects[i].pos, Quaternion.identity);
                NewObject.layer = 9;

                EditorObject eo = NewObject.AddComponent<EditorObject>();
                eo.data.pos = NewObject.transform.position;
                eo.data.objectType = EditorObject.ObjectType.Coin;
            }

            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Player)
            {
                    NewObject = Instantiate(Player, level.editorObjects[i].pos, Quaternion.identity);
                    NewObject.layer = 9; // set to Spawned Objects layer
                    PlayerPlaced = true;

                    EditorObject eo = NewObject.AddComponent<EditorObject>();
                    eo.data.pos = NewObject.transform.position;
                    eo.data.objectType = EditorObject.ObjectType.Player;
            }

            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.StartPos)
            {
                    NewObject = Instantiate(StartPos, level.editorObjects[i].pos, Quaternion.identity);
                    NewObject.layer = 9; // set to Spawned Objects layer
                    StartpointPresent = true;

                    EditorObject eo = NewObject.AddComponent<EditorObject>();
                    eo.data.pos = NewObject.transform.position;
                    eo.data.objectType = EditorObject.ObjectType.StartPos;
            }

            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.EndPos)
            {
                    NewObject = Instantiate(EndPos, level.editorObjects[i].pos, Quaternion.identity);
                    NewObject.layer = 9; // set to Spawned Objects layer
                    EndPointPlaced = true;

                    EditorObject eo = NewObject.AddComponent<EditorObject>();
                    eo.data.pos = NewObject.transform.position;
                    eo.data.objectType = EditorObject.ObjectType.EndPos;
            }
        }
        LoadlevelName.text = "";
        LoadlevelName.DeactivateInputField();
        SaveLoadMessage.text = " Level Loading done";
    }
}
