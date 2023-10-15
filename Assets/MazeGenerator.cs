using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeScript _mazeCellPrefab;
    
    [SerializeField]
    private int _mazeWidth;
    
    [SerializeField]
    private int _mazeLength;
    
    [SerializeField]
    private int _mazeHeight;

    private MazeScript[,,] _mazeGrid;
    
    
    
    // Start is called before the first frame update
    IEnumerator Start() {
        _mazeGrid = new MazeScript[_mazeWidth, _mazeLength, _mazeHeight];
        for(int x = 0; x < _mazeWidth; x++) {
            for(int z = 0; z < _mazeHeight; z++) {
                for(int y = 0; y < _mazeLength; y++) {
                    _mazeGrid[x,y,z] = Instantiate(_mazeCellPrefab, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }

        yield return GenerateMaze(null, _mazeGrid[0,0,0]);
        
    }

    private IEnumerator GenerateMaze(MazeScript previousCell, MazeScript currentCell) {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        yield return new WaitForSeconds(0.05f);

        var next = GetNextUnvisitedCell(currentCell);
        if(next != null) {
            yield return GenerateMaze(currentCell, next);
        }
        
    }

    private MazeScript GetNextUnvisitedCell(MazeScript currentCell) {
        var unvisited = GetUnvisited(currentCell);
        return unvisited.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
        
    }

    private IEnumerable<MazeScript> GetUnvisited(MazeScript current) {
        int in_x = (int) current.transform.position.x;
        int in_y = (int) current.transform.position.y;
        int in_z = (int) current.transform.position.z;
        for(int x = (int) current.transform.position.x; x < in_x; x ++) {
            for(int y = (int) current.transform.position.y; y < in_y; y ++) {
                for(int z = (int) current.transform.position.z; z < in_z; z ++) {
                    if (x < _mazeLength && y < _mazeWidth && z < _mazeHeight && x >=0 && z >= 0 && y >=0) {
                        var curCel = _mazeGrid[x, y, z];
                        if (curCel.IsVisited == false)
                        {
                            yield return curCel;
                        }
                    }
                }
            }
        }
    }

    private void ClearWalls(MazeScript previousCell, MazeScript currentCell) {
        if(previousCell == null) {
            return;
        }
        if(previousCell.transform.position.x < currentCell.transform.position.x) {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        if(previousCell.transform.position.x > currentCell.transform.position.x) {
            currentCell.ClearRightWall();
            previousCell.ClearLeftWall();
            return;
        }
        if(previousCell.transform.position.z < currentCell.transform.position.z) {
            currentCell.ClearFrontWall();
            previousCell.ClearBackWall();
            return;
        }
        if(previousCell.transform.position.z > currentCell.transform.position.z) {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        if(previousCell.transform.position.y < currentCell.transform.position.y) {
            currentCell.ClearRoof();
            previousCell.ClearFloor();
            return;
        }
        if(previousCell.transform.position.y > currentCell.transform.position.y) {
            previousCell.ClearRoof();
            currentCell.ClearFloor();
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
