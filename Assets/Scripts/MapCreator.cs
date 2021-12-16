using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class MapCreator : MonoBehaviour
{
    public static MapCreator mapCreator;
    
    public GameObject bronPrefab;
    public GameObject[] mapItemPrefabs;
    public int wallScale = 1;

    public Vector3 playerHome = new Vector3(0, -8, 0);
    public Vector3 enemyHome = new Vector3(0, 8, 0);

    private List<Vector3> _playerBornPosition = new List<Vector3>();
    private List<Vector3> _enemyBornPosition = new List<Vector3>();

    private HashSet<Vector3> _itemPositionList = new HashSet<Vector3>();
    public int tankCount;
    private float _timeCount = 2f;
    private void Awake()
    {
        mapCreator = this;
        CreateBarrier(mapItemPrefabs[5]);
        CreateHome(mapItemPrefabs[1],playerHome,enemyHome,wallScale);
        CreateMapitems(20,mapItemPrefabs[1]);
        CreateMapitems(30,mapItemPrefabs[2]);
        CreateMapitems(30,mapItemPrefabs[3]);
        CreateMapitems(30,mapItemPrefabs[4]);
        CreateTank(_playerBornPosition[Random.Range(0,_playerBornPosition.Count)],true);
    }

    private void Start()
    {
        InvokeRepeating("CreateEnemy",2f,2f);
    }

    private void Update()
    {
        
    }

    /**
     * 创建空气墙
     */
    public void CreateBarrier(GameObject item)
    {
        foreach (var r in ReturnFrenchPosition(23, 19, new Vector3(-11f, -9f, 0)))
        {
            Instantiate(item, r, transform.rotation, transform);
        }
    }

    /**
     * 创建营地壁障
     */
    public void CreateHome(GameObject item, Vector3 playerHome, Vector3 enemyHome,int scale)
    {
        Vector3 screenPisotion = new Vector3();
        var tempPosition = new Vector3();
        scale += 1;

        //一个阵营的位置与该位置所包含的坦克出生点集合
        var bundle = new Dictionary<Vector3, List<Vector3>>();
        bundle.Add(playerHome,_playerBornPosition);
        bundle.Add(enemyHome,_enemyBornPosition);

        _itemPositionList.Add(playerHome);
        _itemPositionList.Add(enemyHome);

        mapItemPrefabs[0].GetComponent<Heart>().isPlayerHeart = true;
        Instantiate(mapItemPrefabs[0], playerHome, transform.rotation, transform);
        mapItemPrefabs[0].GetComponent<Heart>().isPlayerHeart = false;
        Instantiate(mapItemPrefabs[0], enemyHome, transform.rotation, transform);
        
        foreach (var h in bundle)
        {
            tempPosition = h.Key;
            tempPosition.x -= scale;
            tempPosition.y -= scale;
            for (int i = 0; i < scale; i++)
            {
                foreach (var p in ReturnFrenchPosition(2*scale+1-i*2, 2*scale+1-i*2, tempPosition))
                {
                    screenPisotion = Camera.main.WorldToScreenPoint(p);
                    if (_itemPositionList.Contains(p) 
                        || screenPisotion.x > Screen.width || screenPisotion.x < 0 
                        || screenPisotion.y > Screen.height || screenPisotion.y < 0)
                    {
                        continue;
                    }
                    //城墙外一圈用来生成敌人和玩家，不生成其他物件
                    if (i == 0)
                    {
                        _itemPositionList.Add(p);
                        h.Value.Add(p);
                        continue;
                    }
                    Instantiate(item, p, transform.rotation, transform);
                    _itemPositionList.Add(p);
                }
                tempPosition.x += 1;
                tempPosition.y += 1;
            }
        }
    }

    /**
     * 返回矩形围栏坐标
     */
    public HashSet<Vector3> ReturnFrenchPosition(int width,int height,Vector3 startPoint) //(-1,-9)
    {
        var xPs = startPoint.x + width - 1; //1
        var yPs = startPoint.y + height - 1; //11
        var positions = new HashSet<Vector3>();
        var tempVector3 = new Vector3(0,0,0);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tempVector3.x = startPoint.x + i;
                tempVector3.y = startPoint.y + j;
                //如果墙的x,y坐标不等于起始点坐标或者最后一个墙的坐标，则不生成墙
                if (tempVector3.x - startPoint.x != 0
                    && tempVector3.x - xPs != 0
                    && tempVector3.y - startPoint.y !=0
                    && tempVector3.y - yPs != 0)
                {
                    continue;
                }
                positions.Add(tempVector3);
            }
        }
        return positions;
    }

    /**
     * 产生随机位置
     */
    public Vector3 CreateRandomPosition()
    {
        Vector3 randomPosition;
        do
        {
            randomPosition = new Vector3(Random.Range(-10, 11), Random.Range(-8, 9), 0);
        } while (_itemPositionList.Contains(randomPosition));

        _itemPositionList.Add(randomPosition);
        return randomPosition;
    }


    /**
     * 产生地图上的物件
     */
    public void CreateMapitems(int numbers,GameObject item)
    {
        for (int i = 0; i < numbers; i++)
        {
            Instantiate(item, CreateRandomPosition(), transform.rotation, transform);
        }
    }

    /**
     * 产生坦克
     */
    public void CreateTank(Vector3 position,bool isPlayer)
    {
        bronPrefab.GetComponent<Born>()._isPlayer = isPlayer;
        Instantiate(bronPrefab, position, transform.rotation);
    }

    /**
     * 敌军坦克总数为tankCount，不足tankCount则每隔两秒产生一个坦克
     */
    public void CreateEnemy()
    {
        CreateTank(_enemyBornPosition[Random.Range(0,_enemyBornPosition.Count)],false);
    }
}
