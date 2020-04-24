using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour
{
    public enum LevelManipulation { Create, Destroy }; // the possible level manipulation types
    public enum ItemList { StartPos, EndPos, Platform, Coin, Player }; // the list of items

    [HideInInspector] // we hide these to make them known to the rest of the project without them appearing in the Unity editor.
    public ItemList itemOption = ItemList.Platform; // setting the platform as the default object
    [HideInInspector]
    public LevelManipulation manipulateOption = LevelManipulation.Create; // create is the default manipulation type.
    [HideInInspector]
    public SpriteRenderer sr;

    public Material goodPlace;
    public Material badPlace;
    public GameObject Player;
    public GameObject Platform;
    public GameObject Coin;
    //public ManagerScript ms;

    private Vector2 mousePos;
    private bool colliding;
    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // get the sprite renderer component and store it in sr.
    }

    // Update is called once per frame
    void Update()
    {
        // Have the object follow the mouse cursor by getting mouse coordinates and converting them to world point.
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector2(
            Mathf.Clamp(mousePos.x, -8.5f, 8.5f),
            Mathf.Clamp(mousePos.y, -3.53f, 5.81f)); // limit object movement to minimum -8 and maximum 8 for both x and y coordinates.

        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // send out raycast to detect objects
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == 9) // check if raycast hitting user created object.
            {
                colliding = true; // Unity now knows it cannot create any new object until collision is false.
                sr.material = badPlace; // change the material to red, indicating that the user cannot place the object there.
            }
            else
            {
                colliding = false;
                sr.material = goodPlace;
            }
        }

        // after pressing the left mouse button...
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // check if mouse over UI object.
            {
                if (colliding == false && manipulateOption == LevelManipulation.Create) // create an object if not colliding with anything.
                    CreateObject();
                else if (colliding == true && manipulateOption == LevelManipulation.Destroy) // select object under mouse to be destroyed.
                {
                    if (hit.collider.gameObject.name.Contains("PlayerModel")) // if player object, set ms.playerPlaced to false indicating no player object in level.
                        //ms.playerPlaced = false;

                    Destroy(hit.collider.gameObject); // remove from game.
                }

            }
        }
    }


    /// <summary>
    /// Object creation
    /// </summary>
    void CreateObject()
    {
        GameObject newObj;

        if (itemOption == ItemList.Platform) // Platform
        {
            //Create object
            newObj = Instantiate(Platform, transform.position, Quaternion.identity);
            newObj.layer = 9; // set to Spawned Objects layer

            //Add editor object component and feed it data.
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.pos = newObj.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Platform;
        }
        else if (itemOption == ItemList.Coin) // cube
        {
            //Create object
            newObj = Instantiate(Coin, transform.position, Quaternion.identity);
            newObj.layer = 9; // set to Spawned Objects layer

            //Add editor object component and feed it data.
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.pos = newObj.transform.position;
            eo.data.objectType = EditorObject.ObjectType.Coin;
        }

        else if (itemOption == ItemList.Player) // player start
        {
            //if (ms.playerPlaced == false) // only perform next actions if player not yet placed.
            {
                //Create object and give it capsule collider component.
                newObj = Instantiate(Player, transform.position, Quaternion.identity);
                newObj.layer = 9; // set to Spawned Objects layer
                newObj.AddComponent<CapsuleCollider>();
                newObj.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
                newObj.GetComponent<CapsuleCollider>().height = 2;
               /// ms.playerPlaced = true;

                //Add editor object component and feed it data.
                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.pos = newObj.transform.position;
                eo.data.objectType = EditorObject.ObjectType.Player;
            }
        }
    }
}
