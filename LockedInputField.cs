/* 
 * Filename: "LockedInputField.cs"
 * Created by: Grant A. Mooney
 * Created on: 06/01/2016
 * Description: A child class of Unity's InputField that locks input after user has stopped typing for X seconds. User can add characters, but cannot delete/overwrite locked input.
 * Unity version: 5.3.2f1
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LockedInputField : InputField
{
    #region Public fields
    public float lockTime = 5f; // In seconds
    #endregion

    #region Private fields
    private bool hasInput = false;
    private float inputTime = 0f;
    private string lockedString = "";
    #endregion

    #region Monobehavior Method Overrides
    protected override void Start()
    {
        base.Start();
        this.onValueChanged.AddListener(delegate { OnInput(); });
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (hasInput && ((Time.time - inputTime) > lockTime))
        {
            lockedString = this.text;
            hasInput = false;
        }
    }
    #endregion

    #region Event Delegates
    private void OnInput()
    {
        if (string.IsNullOrEmpty(this.text)) {
            hasInput = false;
            return; 
        }
        inputTime = Time.time;
        hasInput = true;
        if ((lockedString.Length > 0) && (this.text.IndexOf(lockedString) != 0))
        {
            this.text = lockedString;
            this.MoveTextEnd(false);
        }
    }
    #endregion
}
