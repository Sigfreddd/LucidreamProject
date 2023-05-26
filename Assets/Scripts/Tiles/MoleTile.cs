using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class MoleTile : Tile
{
    //[SerializeField]
    //private Mole mole;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);  
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        //Instantiate(mole, position, Quaternion.identity);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Custom Tiles/MoleTile")]
    public static void CreateVariableTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Mole Tile", "New Mole Tile", "Asset", "Save Mole Tile", "Assets");
        if (path == "")
            return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<MoleTile>(), path);
    }
#endif
}
