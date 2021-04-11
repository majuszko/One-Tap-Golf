using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private int dotNumber;
    [SerializeField] GameObject dotParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] private float dotSpacing;

    private Transform[] dotsList;
    private Vector2 pos;
    private float timeStamp;
    
    void Start()
    {
        Hide();
        DotPrep();
    }

    void DotPrep()
    {
        dotsList = new Transform[dotNumber];
        for (int i = 0; i < dotNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotParent.transform;
        }
    }

    public void UpdateDots (Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotNumber; i++) {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            dotsList [i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotParent.SetActive(true);
    }

    public void Hide()
    {
        dotParent.SetActive(false);
    }
}
