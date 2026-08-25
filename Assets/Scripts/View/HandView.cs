using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;

    private readonly List<CardView> cards = new();

    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardPosition(0.15f);
    }

    private IEnumerator UpdateCardPosition(float duration)
    {
        if(cards.Count == 0) yield break;
        float cardSpacing = 1f/7;
        float firstCardPosition = 0.5f - (cards.Count-1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for(int i = 0; i < cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion roation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);
        }
    }
}
