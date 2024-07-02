using Cuebat.Algorithms.ShortestPath;
using TMPro;
using UnityEngine;

public class ShortestPath_BruteForce_Visuals : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeVisual;
    [SerializeField] TextMeshProUGUI countNumber;

    [SerializeField] LineRenderer lineRenderer;

    public ShortestPath_BruteForce SP_BruteForce;
    void Start()
    {
        SP_BruteForce = GetComponent<ShortestPath_BruteForce>();
    }
    void Update()
    {
        DrawPath();
    }



    private void DrawPath()
    {
        lineRenderer.positionCount = SP_BruteForce.shortestPathLocations.Length;
        for (int i = 0; i < SP_BruteForce.shortestPathLocations.Length; i++)
        {
            lineRenderer.SetPosition(i, SP_BruteForce.locations[SP_BruteForce.shortestPathLocations[i]].position);
        }
        UpdateVisuals();
    }


    public void UpdateVisuals()
    {
        timeVisual.text = SP_BruteForce.time.ToString();
        countNumber.text = SP_BruteForce.searchCount.ToString();
    }
}
