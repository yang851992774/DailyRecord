using UnityEngine;

public class FormationRenderer : MonoBehaviour
{
    private TextureFormation _formation;
    public TextureFormation Formation
    {
        get
        {
            if (_formation == null) _formation = GetComponent<TextureFormation>();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] private Vector3 _unitGizmoSize;
    [SerializeField] private Color _gizmoColor;

    private void OnDrawGizmos()
    {
        if (Formation == null || Application.isPlaying) return;
        Gizmos.color = _gizmoColor;

        foreach (var pos in Formation.EvaluatePoints())
        {
            Gizmos.DrawCube(transform.position + pos + new Vector3(0, _unitGizmoSize.y * 0.5f, 0), _unitGizmoSize);
        }
    }
}