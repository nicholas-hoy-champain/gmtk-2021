using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    const int MAX_ALLYNUM = 50;
    public static List<GameObject> AllyRoster = new List<GameObject>();
    public GameObject allyFolder;
    [SerializeField] int soliders;
    [SerializeField] public float size;
    public GameObject soliderPrefab;

    [HideInInspector]
    public bool restructure;

    [HideInInspector]
    public List<Vector3> offsets;

    [HideInInspector]
    public static GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetOffsets();
        restructure = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (restructure)
        {
            RestructureTheAllies();
            restructure = false;
        }
    }

    private void OnConnectedToServer()
    {
        
    }

    static public void AddSoliders(int plan)
    {
        if(instance.allyFolder.transform.childCount + plan > MAX_ALLYNUM)
        {
            plan = MAX_ALLYNUM - instance.allyFolder.transform.childCount;
        }

        for(int i = 0; i < plan; i++)
        Instantiate(instance.soliderPrefab, player.transform.position, player.transform.rotation, instance.allyFolder.transform);

        instance.RestructureTheAllies();
    }

    void RestructureTheAllies()
    {
        foreach (AllyLocationFollower e in GameObject.FindObjectsOfType<AllyLocationFollower>())
        {
            e.RetrieveOffset();
            //e.GetComponent<AllyEdgeDeterminer>().CheckForEdge();
        }

        foreach (AllyEdgeDeterminer e in GameObject.FindObjectsOfType<AllyEdgeDeterminer>())
        {
            e.CheckForEdge();
        }

        HUDmanager.ChangeSoliderNumber(allyFolder.transform.childCount);
    }

    void SetOffsets()
    {
        offsets.Clear();
        Vector3 offset = Vector3.zero;
        int layer = 1, direction = 0, shift = 0, layerFill = 0, layerSize = 8;

        float xShift;

        for (int i = 0; i < MAX_ALLYNUM; i++)
        {
            if (layerFill == layerSize)
            {
                layer++;
                layerSize = 8 * layer;
                layerFill = 0;
                direction = 0;
                shift = 0;
            }

            if (direction == 4)
            {
                direction = 0;
                shift++;
            }

            xShift = Mathf.Pow(-1, shift + 1) * Mathf.Floor(shift / 2.0f + .5f);

            offset.x = xShift;
            offset.y = layer;

            if (direction == 1)
                offset = new Vector3(-offset.y, offset.x);
            else if (direction == 3)
                offset = new Vector3(offset.y, -offset.x);
            else if (direction == 2)
                offset = -offset;

            offsets.Add(offset * size);

            direction++;
            layerFill++;
        }
    }
}
