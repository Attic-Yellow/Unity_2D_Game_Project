using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SnowManProfile : MonoBehaviour
{
    public TMP_Text snowManName;
    public TMP_Text snowManStatus;
    public TMP_Text useText;

    [Header("Snow Man Image")]
    public Image imgBody;
    public Image imgHead;
    public Image imgRightEight;
    public Image imgLeftEight;
    public Image imgHat;

    public Button useButton;
}
