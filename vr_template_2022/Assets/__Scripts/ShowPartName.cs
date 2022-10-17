using UnityEngine;
using TMPro;

public class ShowPartName : MonoBehaviour
{
    [SerializeField] TMP_Text boneName;
    // set this scpt at parent seperate object

    private void Start()
    {
        boneName.SetText("");
    }

    public void setTextName(GameObject childSend)
    {
        boneName.SetText(childSend.name);
    }

    public void resetName()
    {
        boneName.SetText("");
    }
}
