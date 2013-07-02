using UnityEngine;
using System.Collections;

public class SegmentGenerator : MonoBehaviour {
	
	public Segment[] segmentObjects;//these are the objects that are current within the segment generator and will be pooled around
	
	private float spawnZBoundary = 500.0f;
		
	// Use this for initialization
	void Start () {
	
	}

	private int currTerrainIndex = 0;
	
	private float currTerrainDepth = 0.0f;
	
	void AddNewTerrain()
	{
		currTerrainIndex++;
		currTerrainIndex%=segmentObjects.Length;
		
		Segment currSegment = segmentObjects[currTerrainIndex];
		currSegment.Reset();
		currSegment.transform.parent = gameObject.transform;
		
		currSegment.transform.position = new Vector3(0,0, currTerrainDepth + currSegment.depth*0.5f);
		currTerrainDepth+=currSegment.depth;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (!GameManager.IsPaused())
		{
			//update the terrain objects
						
			if (currTerrainDepth - Camera.main.transform.position.z < spawnZBoundary && segmentObjects.Length > 0)
			{
				
				AddNewTerrain();
			}
			
		}
	}
}
