using System.Collections;
using System.Collections.Generic;

public interface IStateEnemy<T>
{
    public void OnInit (T t);
    public void OnExcute(T t);
    public void OnOut(T t);
}
