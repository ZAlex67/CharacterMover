using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private string _axisHoriz = "Horizontal";
    private string _axisVert = "Vertical";

    private Vector3 _mover;

    public Vector3 Mover => _mover;

    private void Update()
    {
        _mover = new Vector3(Input.GetAxis(_axisHoriz), 0, Input.GetAxis(_axisVert));
    }
}