using System.Collections;
using System.Collections.Generic;
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
    void Start() {
        _mazeGrid = new MazeScript[_mazeWidth, _mazeLength, _mazeHeight];
        for(int x = 0; x < _mazeWidth; x++) {
            for(int y = 0; y < _mazeLength; y++) {
                for(int z = 0; z < _mazeHeight; z++) {
                    _mazeGrid[x,y,z] = Instantiate(_mazeCellPrefab, new Vector3(x, y, z), Quaternion.identity);
                    
                    
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
