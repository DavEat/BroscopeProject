using UnityEngine;
using System.Collections.Generic;

public class AvatarList : MonoBehaviour {

    [SerializeField]
    private List<GameObject> listAvatar;

    public List<GameObject> GetAvatarList()
    {
        return listAvatar;
    }
}
