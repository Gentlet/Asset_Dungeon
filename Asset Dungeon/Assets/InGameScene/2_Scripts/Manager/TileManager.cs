using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileProperty
{
    private Vector3Int position;
    private Vector3 relative_position;
    //private string type;
    //private string deployment;

    public void Init(Vector3 position, string type)
    {
        Init(position, type);
    }

    public void Init(Vector3Int position/*, string type, string deployment*/)
    {
        //    if (this.type == null)
        //    {
        this.position = position;
            //this.type = type;
            //this.deployment = deployment;

            relative_position = new Vector3((float)position.x / TileManager.Instance.maxtilesize.x, (float)position.y / TileManager.Instance.maxtilesize.y, 0);
        //}
        //else
        //    Debug.Log("This location is already defined.  (position : " + position + ")");
    }

    public bool ComparePosition(Vector3 pos)
    {
        return ComparePosition(new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z));
    }

    public bool ComparePosition(Vector3Int pos)
    {
        return position == pos ? true : false;
    }

    //public bool CompareType(string ty)
    //{
    //    return type == ty ? true : false;
    //}

    #region Property

    public Vector3Int Position
    {
        get
        {
            return position;
        }
    }

    public Vector3 RelativePosition
    {
        get
        {
            return relative_position;
        }
    }

    //public string Type
    //{
    //    get
    //    {
    //        return type;
    //    }
    //}

    //public string Deployment
    //{
    //    get
    //    {
    //        return deployment;
    //    }
    //}
#endregion
}

public class TilemapData
{
    private string name;

    private Tilemap tilemap;
    private List<TileProperty> tiles = new List<TileProperty>();

    public void Init(string tilename, Tilemap map)
    {
        name = tilename;
        tilemap = map;

        InitTileProperty();
    }

    private void InitTileProperty()
    {
        for (int x = TileManager.Instance.mintilesize.x; x < TileManager.Instance.maxtilesize.x; x++) 
        {
            for (int y = TileManager.Instance.mintilesize.y; y < TileManager.Instance.maxtilesize.y; y++)
            {
                if (tilemap.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    TileProperty tmp = new TileProperty();
                    //string[] types = WallDeployment(new Vector3Int(x, y, 0), tilemap);

                    tmp.Init(new Vector3Int(x, y, 0)/*, types[0], types[1]*/);

                    tiles.Add(tmp);
                }
            }
        }
    }

    //private string[] WallDeployment(Vector3Int position, Tilemap tilemap)
    //{
    //    string composition = string.Empty;
    //    string result = "None";

    //    for (int x = -1; x <= 1; x++)
    //    {
    //        for (int y = -1; y <= 1; y++)
    //        {
    //            if (x != 0 || y != 0)
    //            {
    //                if (tilemap.HasTile(new Vector3Int(position.x + x, position.y + y, position.z)))
    //                {
    //                    composition += 'W';
    //                }
    //                else
    //                {
    //                    composition += 'E';
    //                }
    //            }
    //        }
    //    }

    //    if (composition[1] == 'W' || composition[6] == 'W')
    //    {
    //        result = "Transverse";
    //    }
    //    else if (composition[3] == 'W' || composition[4] == 'W')
    //    {
    //        result = "Length";
    //    }

    //    if ((composition[1] == 'W' || composition[6] == 'W') && (composition[3] == 'W' || composition[4] == 'W'))
    //    {
    //        int x = 0;
    //        int y = 0;

    //        if (composition[1] == 'W') x += 1;
    //        if (composition[6] == 'W') x += 1;

    //        if (composition[3] == 'W') y += 1;
    //        if (composition[4] == 'W') y += 1;

    //        if (x + y == 2)
    //        {
    //            if (composition[1] == 'W' && composition[3] == 'W') result = "CrossRightTop";
    //            else if (composition[6] == 'W' && composition[3] == 'W') result = "CrossLeftTop";
    //            else if (composition[1] == 'W' && composition[4] == 'W') result = "CrossRightBottom";
    //            else if (composition[6] == 'W' && composition[4] == 'W') result = "CrossLeftBottom";
    //        }

    //        else if (x + y == 3)
    //        {
    //            if (x >= 2) result = "Transverse";
    //            else if (y >= 2) result = "Length";
    //        }
    //        else
    //            result = "Cross";
    //    }

    //    return new string[2] { result, composition };
    //}

    public TileProperty GetClosestTile(Vector3 position)
    {
        return GetClosestTile(new Vector3Int((int)position.x, (int)position.y, (int)position.z));
    }

    public TileProperty GetClosestTile(Vector3Int position)
    {
        List<TileProperty> closesttiles = new List<TileProperty>();

        for (int i = 1; i < i + 1; i++)
        {
            for (int x = -1 * i; x <= 1 * i; x++)
            {
                for (int y = -1 * i; y <= 1 * 1; y++)
                {
                    Vector3Int pos = new Vector3Int(position.x + x, position.y + y, position.z);

                    if (GetTile(pos) != null)
                        closesttiles.Add(GetTile(pos));

                }
            }
            if (0 < closesttiles.Count)
            {
                TileProperty tmp = closesttiles[0];
                closesttiles.Remove(closesttiles[0]);

                for (int j = 0; j < closesttiles.Count; j++)
                {
                    if (Vector3.Distance(position, closesttiles[j].Position) < Vector3.Distance(position, tmp.Position))
                    {
                        tmp = closesttiles[j];
                    }
                }

                return tmp;
            }
        }

        return null;
    }


    public TileProperty GetTile(int num)
    {
        return tiles[num];
    }

    public TileProperty GetTile(Vector3Int position)
    {
        if(position.x < 0 && position.y < 0)
        {
            Debug.LogError("Not Allowed position is used  (" + position + ")");
            return null;
        }


        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].ComparePosition(position))
                return tiles[i];
        }

        return null;
    }

    public TileProperty GetTile(Vector3 position)
    {
        return GetTile(new Vector3Int((int)position.x, (int)position.y, (int)position.z));
    }

    public List<TileProperty> GetTiles()
    {
        return tiles;
    }

    #region Property
    public string Name
    {
        get
        {
            return name;
        }
    }

    public Tilemap Tilemap
    {
        get
        {
            return tilemap;
        }
    }
    #endregion
}

public class TileManager : SingletonGameObject<TileManager> {

    public GameObject grid;
    public Vector3Int mintilesize;
    public Vector3Int maxtilesize;

    private List<TilemapData> tilemaplist;

    private void Awake()
    {
        tilemaplist = new List<TilemapData>();
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            AddTileMap(grid.transform.GetChild(i).name, grid.transform.GetChild(i).GetComponent<Tilemap>());
        }
    }

    private void AddTileMap(string name, Tilemap tilemap)
    {
        if (GetTilemap(name) == null)
        {
            TilemapData tmp = new TilemapData();
            tmp.Init(name, tilemap);
            tilemaplist.Add(tmp);
        }
    }

    public TilemapData GetTilemap(string name)
    {
        for (int i = 0; i < tilemaplist.Count; i++)
        {
            if (tilemaplist[i].Name == name)
                return tilemaplist[i];
        }

        return null;
    }

    public TilemapData GetTilemap(int num)
    {
        return tilemaplist[num];
    }
}
