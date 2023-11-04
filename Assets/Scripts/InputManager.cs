using UnityEngine;

public static class InputManager
{
    private static Controls _ctrls;

    private static Vector3 _mousePos;

    private static Camera _camera;
    public static Ray GetCameraRay()
    {
        return _camera.ScreenPointToRay(_mousePos);
    }

    public static void Init(Player p)
    {
        _ctrls = new();
        _camera = Camera.main;
        _ctrls.Permenanet.Enable();

        _ctrls.InGame.Shoot.performed += _ =>
        {
            p.Shoot();
        };
        _ctrls.Permenanet.MousePos.performed += ctx =>
        {
            _mousePos = ctx.ReadValue<Vector2>();
        };
        _ctrls.InGame.Reload.performed += _ =>
        {
            p.reload();
        };
    }

    public static void EnableInGame()
    {
        _ctrls.InGame.Enable();
    }
}
