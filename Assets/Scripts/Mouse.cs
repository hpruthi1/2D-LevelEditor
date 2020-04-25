using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public enum ItemList { StartPos, EndPos, Platform, Coin, Player }; // the list of items

    [HideInInspector]
    public ItemList itemOption = ItemList.Platform; // setting the platform as the default object

    public GameObject Player;
    public GameObject Platform;
    public GameObject Coin;
    public GameObject StartPos;
    public GameObject EndPos;
    public ManagerScript ms;

    private Vector2 mousePos;
    public bool Colliding;
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
            if (!EventSystem.current.IsPointerOverGameObject()) // check if mouse over UI object.
            {
                if (Colliding == false) // create an object if not colliding with anything.
                    CreateObject();
            }
        }

        if (Input.GetMouseButton(1))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            if (Physics2D.Raycast(mousePos, Camera.main.transform.forward)){
                if (hit.collider.gameObject.layer == 9)
                {
                    Destroy(hit.collider.gameObject);
                }  
            }
        }
 
    }

    void CreateObject()
    {
        GameObject NewObject;

        if (itemOption == ItemList.Platform) // Platform
        {
            //Create object
            NewObject = Instantiate(Platform, transform.position, Quaternion.identity);
            NewObject.layer = 9; // set to Spawned Objects layer

            //Add editor object component and feed it data.
            EditorObject eo = NewObject.AddComponent<EditorObject>();
            eo.data.pos = NewObject.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Platform;
        }
        else if (itemOption == ItemList.Coin) // coin
        {
            //Create object
            NewObject = Instantiate(Coin, transform.position, Quaternion.identity);
            NewObject.layer = 9; // set to Spawned Objects layer

            //Add editor object component and feed it data.
            EditorObject eo = NewObject.AddComponent<EditorObject>();
            eo.data.pos = NewObject.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Coin;
        }

        else if (itemOption == ItemList.Player) // player start
        {
            if (ms.StartpointPresent)
            {
                //Create object
                NewObject = Instantiate(Player, GameObject.FindGameObjectWithTag("Starting").transform.position, Quaternion.identity);
                NewObject.layer = 9; // set to Spawned Objects layer
                ms.spriteRenderer.sprite = null;

                //Add editor object component and feed it data.
                EditorObject eo = NewObject.AddComponent<EditorObject>();
                eo.data.pos = NewObject.transform.position;
                eo.data.objectType = EditorObject.ObjectType.Player;
            }
            else
            {
                ms.MessagePopup.SetActive(true);
            }
        }

        else if (itemOption == ItemList.StartPos)
        {
            //Create object
            NewObject = Instantiate(StartPos, transform.position, Quaternion.identity);
            NewObject.layer = 9; // set to Spawned Objects layer
            ms.StartpointPresent = true;

            //Add editor object component and feed it data.
            EditorObject eo = NewObject.AddComponent<EditorObject>();
            eo.data.pos = NewObject.transform.position;
            eo.data.objectType = EditorObject.ObjectType.StartPos;
        }

        else if (itemOption == ItemList.EndPos)
        {
            //Create object
            NewObject = Instantiate(EndPos, transform.position, Quaternion.identity);
            NewObject.layer = 9; // set to Spawned Objects layer

            //Add editor object component and feed it data.
            EditorObject eo = NewObject.AddComponent<EditorObject>();
            eo.data.pos = NewObject.transform.position;
            eo.data.objectType = EditorObject.ObjectType.EndPos;
        }
    }

 }
