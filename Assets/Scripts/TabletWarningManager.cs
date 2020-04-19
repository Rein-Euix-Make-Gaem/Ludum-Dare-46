using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletWarningManager : MonoBehaviour
{
    public GameObject ReactorWarningObject;
    public GameObject LargeHoleWarningObject;
    public GameObject SmallHoleWarningObject;
    public GameObject AsteroidFieldWarningObject;

    // Start is called before the first frame update
    void Start()
    {
        this.ReactorWarningObject.SetActive(false);
        this.LargeHoleWarningObject.SetActive(false);
        this.SmallHoleWarningObject.SetActive(false);
        this.AsteroidFieldWarningObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
