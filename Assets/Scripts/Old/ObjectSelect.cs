using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelect : MonoBehaviour, IPointerClickHandler
{
    private Renderer materialRenderer;
    private Color originalColor;
    private Color highlightColor;

    [SerializeField][Tooltip("Color of the object when the wrong formula is used.")]
    private Color incorrectColor;
    [SerializeField]
    [Tooltip("Color of the object when the wrong formula is used.")]
    private Color correctColor;

    private Color incorrectTextColor;
    private Color correctTextColor;

    public bool IsSelected = false;
    [SerializeField]
    private bool IsCorrect = false;
    private string formula = "";
    private bool IsFading = false;

    /// <summary>
    /// Return wheter it had the correct formula
    /// </summary>
    /// <returns>bool</returns>
    public bool GetCorrect()
    {
        return IsCorrect;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject() || IsFading) return;

        IsSelected = !IsSelected;
        if (IsSelected)
        {
            materialRenderer.material.color = highlightColor;
            CheckCorrect();
        }
        else
        {
            //reset the surface
            materialRenderer.material.color = originalColor;
            IsCorrect = false;
        }
    }

    /*
    private void OnMouseDown()
    {
        IsSelected = !IsSelected;
       if (IsSelected)
       {
           materialRenderer.material.color = highlightColor;
            CheckCorrect();
        }
        else
        {
            materialRenderer.material.color = originalColor;
			floatingHint.UpdateText("");
            IsCorrect = false;
        }
    }*/

    /// <summary>
    /// Reset the color back to its original or when incorrect to the incorrect color
    /// </summary>
    public void ConfirmReset()
    {
        if (!IsSelected) return;

        IsSelected = false;
        if (!IsCorrect)
        {
            //Notify user that it is incorrect;
            materialRenderer.material.color = incorrectColor;
        }
        else
        {
            materialRenderer.material.color = correctColor;
        }   
    }

    /// <summary>
    /// Check wether the currently selected formula is the correct formula
    /// </summary>
    public void CheckCorrect()
    {
    }

    public void StartFade()
    {
        IsFading = true;
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        materialRenderer.material.color = correctColor;
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
}
