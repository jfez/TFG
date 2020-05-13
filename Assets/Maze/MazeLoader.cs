using UnityEngine;
using System.Collections;
using UnityEditor;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public float size = 2f;

	private MazeCell[,] mazeCells;

	public GameObject mazeParent;

	// Use this for initialization
	void Start () 
	{
		StartMaze();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
		{
			StartMaze();
			Debug.Log("RESTART");
			string localPath = "Assets/" + mazeParent.name + ".prefab";
			localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
			PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, InteractionMode.UserAction);
		}
	}

	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				// For now, use the same wall object for the floor!
				mazeCells [r, c] .floor = Instantiate (wall, new Vector3 (r*size, -(size/2f), c*size) + mazeParent.transform.position, Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);
				mazeCells [r, c] .floor.transform.parent = mazeParent.transform;

				if (c == 0) 
				{
					if (r != 0)
					{
						mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)) + mazeParent.transform.position, Quaternion.identity) as GameObject;
						mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
						mazeCells[r,c].westWall.transform.parent = mazeParent.transform;
					}
					
					
				}

				if (c != mazeColumns - 1 || r != mazeRows - 1)
				{
					mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)) + mazeParent.transform.position, Quaternion.identity) as GameObject;
					mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;
					mazeCells [r, c].eastWall.transform.parent = mazeParent.transform;
				}
				
				

				if (r == 0) 
				{
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size) + mazeParent.transform.position, Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
					mazeCells [r, c].northWall.transform.parent = mazeParent.transform;
				}

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size) + mazeParent.transform.position, Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
				mazeCells[r,c].southWall.transform.parent = mazeParent.transform;
			}
		}

		mazeParent.transform.Rotate(Vector3.up, 90f);
	}

	private void StartMaze()
	{
		DeleteMaze();
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
	}

	private void DeleteMaze()
	{
		foreach (Transform child in mazeParent.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}
}
