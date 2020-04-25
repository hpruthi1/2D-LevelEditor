using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public Mouse user;
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

    public void ChooseStartPoint()
    {
        user.itemOption = Mouse.ItemList.StartPos; // set object to place as player marker
        spriteRenderer.sprite = StartPos.GetComponent<SpriteRenderer>().sprite;
    }

    public void ChooseEndPoint()
    {
        user.itemOption = Mouse.ItemList.EndPos; // set object to place as player marker
        spriteRenderer.sprite = EndPos.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnContinueButton()
    {
        MessagePopup.SetActive(false);
    }
}
