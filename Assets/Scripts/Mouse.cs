using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public enum ItemList { StartPos, EndPos, Platform, Coin, Player };

    [HideInInspector]
    public ItemList itemOption = ItemList.Platform;

    public GameObject Player;
    public GameObject Platform;
    public GameObject Coin;
    public GameObject StartPos;
    public GameObject EndPos;
    public ManagerScript ms;

    private Vector2 mousePos;
    private Ray ray;

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector2(
            Mathf.Clamp(mousePos.x, -8.5f, 8.5f),
            Mathf.Clamp(mousePos.y, -3.53f, 5.81f)); // limit object movement to minimum and maximum for both x and y coordinates.


        RaycastHit2D hit = Physics2D.Raycast(mousePos, Camera.main.transform.forward);
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics2D.Raycast(mousePos, Camera.main.transform.forward))
                {
                    if (hit.collider.gameObject.name.Contains("Platform") || hit.collider.gameObject.name.Contains("Bronze"))
                    {
                        return;
                    }
                }

                else 
                {
                    CreateObject();
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            if (Physics2D.Raycast(mousePos, Camera.main.transform.forward)){
                if (hit.collider.gameObject.name.Contains("starting")){
                    ms.StartpointPresent = false;
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.name.Contains("Player"))
                {
                    ms.PlayerPlaced = false;
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.name.Contains("End"))
                {
                    ms.EndPointPlaced = false;
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.layer == 9)
                {
                    Destroy(hit.collider.gameObject);
                }  
            }
        }
 
    }

    void CreateObject()
    {
        GameObject NewObject;

        if (itemOption == ItemList.Platform)
        {
            NewObject = Instantiate(Platform, transform.position, Quaternion.identity);
            NewObject.layer = 9;

            EditorObject eo = NewObject.AddComponent<EditorObject>();
            eo.data.pos = NewObject.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Platform;
        }
        else if (itemOption == ItemList.Coin)
        {
            NewObject = Instantiate(Coin, transform.position, Quaternion.identity);
            NewObject.layer = 9;

            EditorObject eo = NewObject.AddComponent<EditorObject>();
            eo.data.pos = NewObject.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Coin;
        }

        else if (itemOption == ItemList.Player)
        {
            if (ms.StartpointPresent && ms.PlayerPlaced == false)
            {
                NewObject = Instantiate(Player, GameObject.FindGameObjectWithTag("Starting").transform.position, Quaternion.identity);
                NewObject.layer = 9;
                ms.spriteRenderer.sprite = null;
                ms.PlayerPlaced = true;

                EditorObject eo = NewObject.AddComponent<EditorObject>();
                eo.data.pos = NewObject.transform.position;
                eo.data.objectType = EditorObject.ObjectType.Player;
            }
            else if (ms.StartpointPresent == false)
            {
                ms.MessagePopup.SetActive(true);
            }
        }

        else if (itemOption == ItemList.StartPos)
        {
            if (ms.StartpointPresent == false)
            {
                NewObject = Instantiate(StartPos, transform.position, Quaternion.identity);
                NewObject.layer = 9;
                ms.StartpointPresent = true;

                EditorObject eo = NewObject.AddComponent<EditorObject>();
                eo.data.pos = NewObject.transform.position;
                eo.data.objectType = EditorObject.ObjectType.StartPos;
            }
        }

        else if (itemOption == ItemList.EndPos)
        {
            if (ms.EndPointPlaced == false)
            {
                NewObject = Instantiate(EndPos, transform.position, Quaternion.identity);
                NewObject.layer = 9;
                ms.EndPointPlaced = true;

                EditorObject eo = NewObject.AddComponent<EditorObject>();
                eo.data.pos = NewObject.transform.position;
                eo.data.objectType = EditorObject.ObjectType.EndPos;
            }
        }
    }

 }
