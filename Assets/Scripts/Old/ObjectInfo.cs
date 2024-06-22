using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    [Tooltip("All the sides of an Geometric Object")]
    public List<GameObject> sides;
    private int sideCount;

    private CheckForObject parentCheck;

    [Tooltip("Do you need to Volume Formula?")]
    public bool IsVolume = false;

    private void Awake()
    {
        sideCount = sides.Count;
    }

    // Start is called before the first frame update
    private void Start()
    {
        parentCheck = GetComponentInParent<CheckForObject>();
    }

    /// <summary>
    /// Set the activity of children based on wether they were correct or not
    /// </summary>
    public void SetActivityChildren()
    {
        if (sides.Count == 0) return;

        ObjectSelect select;

        foreach (GameObject child in sides)
        {
            if (!child.activeInHierarchy) continue;
            select = child.GetComponent<ObjectSelect>();
            select.ConfirmReset();
            if (select.GetCorrect())
            {
                //start timer
                select.StartFade();
                sideCount--;
            }
        }
        StartCoroutine(UpdateDelay());
    }

    IEnumerator UpdateDelay()
    {
        yield return new WaitForSeconds(5.1f);
        UpdateSides();
    }

    /// <summary>
    /// Update the count on how many children are active
    /// </summary>
    public void UpdateSides()
    {
        if(sides.Count == 0)
        {
            //its volume
            //check wether it is selected and correct
            //if correct, delete this and spawn next object
            return;
        }

        //its area
        //check for all side wether it is selected and correct
        //if correct deactivate it,
        //if all sides are deactivated, delete this and spawn next object

        int i = 0;
        foreach (GameObject child in sides)
        {
            if(child.activeInHierarchy) { i++; }
        }

        sideCount = i;

        if(sideCount == 0)
        {
            DeleteObject();
        }
    }

    /// <summary>
    /// Delete the current object and replace it
    /// </summary>
    public void DeleteObject()
    {
        //Create a new object
        parentCheck.RefreshObject();
    }
}
