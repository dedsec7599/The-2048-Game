using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillBlocks : MonoBehaviour
{
  //This class deals with providing blocks with values and tell the CellShift class whether the blocks will merge. It also updates score,handles block colors and tells whether the player won or not.
    public int value;
    [SerializeField] TextMeshProUGUI valueDisplay;
    [SerializeField] float speed;

    bool hasMerged;

    Image blockColor;
    public void FillValueUpdate(int _value)
    {
        value = _value;
        valueDisplay.text = value.ToString();

        int colorIndex = GetColorIndex(value);
        blockColor = GetComponent<Image>();
        blockColor.color = GameController.instance.fillColors[colorIndex];
    }

    int GetColorIndex(int _value)
    {
        int index = 0;
        while (_value != 1)
        {
            index++;
            _value /= 2;
        }

        index--;
        return index;
    }

    private void Update()
    {
        if (transform.localPosition != Vector3.zero)
        {
            hasMerged = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
        }
        else if(hasMerged == false)
        {
            if (transform.parent.GetChild(0) != this.transform)
            {
                Destroy(transform.parent.GetChild(0).gameObject);
            }

            hasMerged = true;
        }
    }

    public void Double()
    {
        value *= 2;
        GameController.instance.ScoreUpdate(value);
        valueDisplay.text = value.ToString();

        int colorIndex = GetColorIndex(value);
        blockColor.color = GameController.instance.fillColors[colorIndex];

        GameController.instance.CheckWin(value);
    }
}
