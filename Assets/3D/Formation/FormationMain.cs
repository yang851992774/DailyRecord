using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class FormationMain : MonoBehaviour
{
    public TextureUnit[] textureUnits = new TextureUnit[0];
    private int curTextureIndex = 0;

    [SerializeField] private TextureFormation formation;
    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private float unitSpeed = 2;


    private readonly List<PlayerUnit> spawnedUnits = new List<PlayerUnit>();
    private List<Vector3> points = new List<Vector3>();
    private Transform parentTrans;



    private void Awake()
    {
        parentTrans = new GameObject("Unit Parent").transform;
        ChangeFormation();
    }

    private void Update()
    {
        GenFormation();

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 200))
            {
                if ("ground" == hitInfo.collider.tag)
                {
                    formation.transform.position = hitInfo.point;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ++curTextureIndex;
            ChangeFormation();
        }


    }

    private void ChangeFormation()
    {
        if (curTextureIndex > (textureUnits.Length - 1))
        {
            curTextureIndex = 0;
        }
        var curTextureUnit = textureUnits[curTextureIndex];
        formation.texture = curTextureUnit.texture;
        formation.samplingStep = curTextureUnit.samplingStep;
        formation.scale = curTextureUnit.scale;
        formation.ReCalculate();
    }

    // 根据点阵图生成角色
    private void GenFormation()
    {
        points = formation.EvaluatePoints().ToList();

        if (points.Count > spawnedUnits.Count)
        {
            var remainingPoints = points.Skip(spawnedUnits.Count);
            SpawnAvatar(remainingPoints);
        }
        else if (points.Count < spawnedUnits.Count)
        {
            DeleteAvatar(spawnedUnits.Count - points.Count);
        }

        for (var i = 0; i < spawnedUnits.Count; i++)
        {
            var unit = spawnedUnits[i];
            // 距离
            var distance = Vector3.Distance(points[i], unit.trans.position);

            if (distance > unitSpeed)
            {
                // 方向
                var dir = points[i] - unit.trans.position;
                // 线性差值设置方向，朝向目标点方向
                unit.trans.forward = Vector3.Lerp(unit.trans.forward, new Vector3(dir.x, 0, dir.z), 5 * Time.deltaTime);
                // 动画混合
                unit.speed = distance > 0.8f ? distance : 0.8f;
                unit.ani.SetFloat("Speed", unit.speed);
                // 移动
                unit.trans.position = unit.trans.position + (points[i] - unit.trans.position).normalized * unitSpeed;
            }
            else
            {
                // 距离很小，直接设置目标点位置
                unit.trans.position = points[i];
                if (unit.speed > 0)
                {
                    // 慢慢过渡为站立
                    unit.speed -= Time.deltaTime * 0.5f;
                    if (unit.speed < 0)
                        unit.speed = 0;
                    unit.ani.SetFloat("Speed", unit.speed);
                }
                // 线性差值设置方向，统一朝向正前方
                unit.trans.forward = Vector3.Lerp(unit.trans.forward, -Vector3.forward, 5 * Time.deltaTime);
            }
        }
    }

    // 生成角色
    private void SpawnAvatar(IEnumerable<Vector3> points)
    {
        foreach (var pos in points)
        {
            var unit = new PlayerUnit();
            var obj = Instantiate(unitPrefab, transform.position + pos, Quaternion.identity, parentTrans);
            unit.obj = obj;
            unit.trans = obj.transform;
            unit.ani = obj.GetComponent<Animator>();


            spawnedUnits.Add(unit);
        }
    }

    // 删除多余的角色
    private void DeleteAvatar(int num)
    {
        for (var i = 0; i < num; i++)
        {
            var unit = spawnedUnits.Last();
            spawnedUnits.Remove(unit);
            Destroy(unit.obj);
        }
    }
}

public class PlayerUnit
{
    public GameObject obj;
    public Transform trans;
    public Animator ani;
    public float speed;
}

[System.Serializable]
public class TextureUnit
{
    public Texture2D texture;
    public int samplingStep;
    public float scale;
}