using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    private string _axisHoriz = "Horizontal";
    private string _axisVert = "Vertical";

    private Vector3 _move;

    public Vector3 Move => _move;

    private void Update()
    {
        _move = new Vector3(Input.GetAxis(_axisHoriz), 0, Input.GetAxis(_axisVert));
    }
}