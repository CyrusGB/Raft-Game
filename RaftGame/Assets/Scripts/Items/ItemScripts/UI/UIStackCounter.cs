using UnityEngine;
using TMPro;

public class UIStackCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stackTxt;

    public void SetStack(int amount){
        stackTxt.text = amount > 1 ? amount.ToString() : "";
    }
}
