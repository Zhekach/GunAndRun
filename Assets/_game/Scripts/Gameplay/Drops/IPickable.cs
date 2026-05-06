using UnityEngine;

public interface IPickable
{
    //TODO refactor как-то убрать геймобджект и добавить строгий тип
    bool Pick(GameObject picker);
}
