using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellShift : MonoBehaviour
{
  //This Class is responsible for shifting the blocks after receiving input from GameController class.
    public CellShift right;
    public CellShift left;
    public CellShift up;
    public CellShift down;

    public FillBlocks fill;

    private void OnEnable()
    {
        GameController.slide += OnSlide;
    }

    private void OnDisable()
    {
        GameController.slide -= OnSlide;
    }

    //Directions are received in recievedDir from GameController class 
    private void OnSlide(string recievedDir)
    {
        CellChecking();
        Debug.Log(recievedDir);
        if (recievedDir == "up")
        {
            if (up != null) return;
            CellShift currentCell = this;
            SlideUp(currentCell);
        }
        if (recievedDir == "down")
        {
            if (down != null) return;
            CellShift currentCell = this;
            SlideDown(currentCell);
        }
        if (recievedDir == "left")
        {
            if (left != null) return;
            CellShift currentCell = this;
            SlideLeft(currentCell);
        }
        if (recievedDir == "right")
        {
            if (right != null) return;
            CellShift currentCell = this;
            SlideRight(currentCell);
        }

        GameController.ticker++;
        if (GameController.ticker == 4)
        {
            GameController.instance.SpawnFill();
        }
    }

    //These for functions below slide all the blocks in the received direction from user while checking whether the blocks can shift or not and which blocks can merge.
    void SlideUp(CellShift currentCell)
    {
        if (currentCell.down == null)
        {
            return;
        }
        if (currentCell.fill != null)
        {
            CellShift nextCell = currentCell.down;
            while (nextCell.down != null && nextCell.fill == null)
            {
                nextCell = nextCell.down;
            }
            if (nextCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if(currentCell.down.fill!=nextCell.fill)
                {
                    Debug.Log("not double");
                    nextCell.fill.transform.parent = currentCell.down.transform;
                    currentCell.down.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            CellShift nextCell = currentCell.down;
            while (nextCell.down != null && nextCell.fill == null)
            {
                nextCell = nextCell.down;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideUp(currentCell);
                //Debug.Log("Slide to empty");
            }
        }

        if (currentCell.down == null) return;
        SlideUp(currentCell.down);
    }

    void SlideDown(CellShift currentCell)
    {
        if (currentCell.up == null)
        {
            return;
        }
        if (currentCell.fill != null)
        {
            CellShift nextCell = currentCell.up;
            while (nextCell.up != null && nextCell.fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.up.fill != nextCell.fill)
                {
                    Debug.Log("not doubled");
                    nextCell.fill.transform.parent = currentCell.up.transform;
                    currentCell.up.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            CellShift nextCell = currentCell.up;
            while (nextCell.up != null && nextCell.fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideDown(currentCell);
                Debug.Log("Slide to empty");
            }
        }

        if (currentCell.up == null) return;
        SlideDown(currentCell.up);
    }

    void SlideRight(CellShift currentCell)
    {
        if (currentCell.left == null)
        {
            return;
        }
        if (currentCell.fill != null)
        {
            CellShift nextCell = currentCell.left;
            while (nextCell.left != null && nextCell.fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.left.fill != nextCell.fill)
                {
                    Debug.Log("not doubled");
                    nextCell.fill.transform.parent = currentCell.left.transform;
                    currentCell.left.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            CellShift nextCell = currentCell.left;
            while (nextCell.left != null && nextCell.fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideRight(currentCell);
                Debug.Log("Slide to empty");
            }
        }

        if (currentCell.left == null) return;
        SlideRight(currentCell.left);
    }

    void SlideLeft(CellShift currentCell)
    {
        if (currentCell.right == null)
        {
            return;
        }
        if (currentCell.fill != null)
        {
            CellShift nextCell = currentCell.right;
            while (nextCell.right != null && nextCell.fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Double();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else if (currentCell.right.fill != nextCell.fill)
                {
                    //Debug.Log("not doubled");
                    nextCell.fill.transform.parent = currentCell.right.transform;
                    currentCell.right.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            CellShift nextCell = currentCell.right;
            while (nextCell.right != null && nextCell.fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideLeft(currentCell);
                //Debug.Log("Slide to empty");
            }
        }

        if (currentCell.right == null) return;
        SlideLeft(currentCell.right);
    }


    //This function to check the board whether it has been completely filled or not and tell if the game is over.
    void CellChecking()
    {
        if (fill == null) return;
        if (up != null)
        {
            if (up.fill == null) return;
            if (up.fill.value == fill.value) return;
        }
        
        if (down != null)
        {
            if (down.fill == null) return;
            if (down.fill.value == fill.value) return;
        }
        
        if (left != null)
        {
            if (left.fill == null) return;
            if (left.fill.value == fill.value) return;
        }
        
        if (right != null)
        {
            if (right.fill == null) return;
            if (right.fill.value == fill.value) return;
        }

        GameController.instance.GameOverCheck();
    }
}
