using System;

public interface IShakeProvider {
    event Action OnShakeStart;
    event Action OnShakeEnd;
}
